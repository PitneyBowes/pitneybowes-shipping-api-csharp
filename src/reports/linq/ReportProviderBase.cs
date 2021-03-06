﻿/*
Copyright 2019 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PitneyBowes.Developer.ShippingApi.Report
{
    /// <summary>
    /// Common functionality for implementing a linq queryable report
    /// </summary>
    public abstract class ReportProviderBase
    {
        /// <summary>
        /// Called by the report to set the linq expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(TransactionsReport<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }
        /// <summary>
        /// Execute the query
        /// </summary>
        /// <typeparam name="TResult">Required result type</typeparam>
        /// <typeparam name="ReportItem">Report item type</typeparam>
        /// <typeparam name="Request"></typeparam>
        /// <typeparam name="RequestFinder">Linq expression visitor - to find the where clause</typeparam>
        /// <param name="expression">Linq expression</param>
        /// <param name="isEnumerable"></param>
        /// <param name="reportService">Method to get an IEmumerable that will iterate the result set from the web service</param>
        /// <param name="initializeRequest">Method to initialize the request.</param>
        /// <returns></returns>
        public object Execute<TResult, ReportItem, Request, RequestFinder>(Expression expression, bool isEnumerable, Func<Request, IEnumerable<ReportItem>> reportService, Action<Request> initializeRequest)
            where Request : IReportRequest, new()
            where RequestFinder : RequestFinderVisitor<Request, ReportItem>, new()
        { 
            if (!(expression is MethodCallExpression))
                throw new InvalidProgramException("No query over the data source was specified.");

            var rf = new RequestFinder() { Expression = GetWhereExpression(expression).Body };
            var request = rf.Request;
            initializeRequest(request);
            if (!request.Validate())
                throw new System.InvalidOperationException("Execute: Request is not valid"); 

            // Call the Web service and get the results.
            var report = reportService(request);

            // Copy the IEnumerable places to an IQueryable.
            IQueryable<ReportItem> queryableTransaction = report.AsQueryable();

            var newExpressionTree = ReplaceWhereByTransaction<TResult, IQueryable<ReportItem>>(expression, queryableTransaction);
            // This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods.
            if (isEnumerable)
                return queryableTransaction.Provider.CreateQuery(newExpressionTree);
            else
                return queryableTransaction.Provider.Execute(newExpressionTree);

        }
        /// <summary>
        /// Copy the expression tree that was passed in, changing only the first 
        /// argument of the innermost MethodCallExpression.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="ReportItem"></typeparam>
        /// <param name="expression"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public Expression ReplaceWhereByTransaction<TResult, ReportItem>(Expression expression, ReportItem items ) 
        {
            // Copy the expression tree that was passed in, changing only the first 
            // argument of the innermost MethodCallExpression.

            var treeCopier = new ReplaceQueryWithResultSetModifier<TResult, ReportItem>(items);
            return treeCopier.Visit(expression);
         }

        /// <summary>
        /// Find the where clause so parameters can be extracted for the web service call
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public LambdaExpression GetWhereExpression(Expression expression)
        {
            // Find the call to Where() and get the lambda expression predicate.
            var whereFinder = new InnermostWhereFinder();
            var whereExpression = whereFinder.GetInnermostWhere(expression);
            var lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // Send the lambda expression through the partial evaluator.
            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);
            return lambdaExpression;
        }


        /// <summary>
        /// Queryable's "single value" standard query operators call this method.
        /// It is also called from QueryableTerraServerData.GetEnumerator(). 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)Execute<TResult>(expression, IsEnumerable);
        }

        /// <summary>
        /// Untyped expression. Not implemented. TODO: Figure out how to know the return type
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual object Execute(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                throw new NotImplementedException(); //TODO: Figure out how to know the return type
                //call internal static object Execute<TResult>(Expression expression, bool IsEnumerable), where typeof(TResult) = elementType;
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        /// <summary>
        /// Abstract Queryable's collection-returning standard query operators call this method. 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public abstract IQueryable<TResult> CreateQuery<TResult>(Expression expression);
        /// <summary>
        /// Abstract - call the web service.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="IsEnumerable"></param>
        /// <returns></returns>
        public abstract object Execute<TResult>(Expression expression, bool IsEnumerable);
    }


}

