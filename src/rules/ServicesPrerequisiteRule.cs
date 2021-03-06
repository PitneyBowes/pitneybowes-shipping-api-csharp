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

using Newtonsoft.Json;

namespace PitneyBowes.Developer.ShippingApi.Rules
{
    /// <summary>
    /// Prerequisites for applying the special service. If you select this special service, you must also select the other special 
    /// services in this array.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ServicesPrerequisiteRule 
    {
        /// <summary>
        /// Id of the service
        /// </summary>
        [JsonProperty("specialServiceId")]
        virtual public SpecialServiceCodes SpecialServiceId{get; set;}
        /// <summary>
        /// Minimul value, if numeric
        /// </summary>
        [JsonProperty("minInputValue")]
        virtual public decimal MinInputValue{get; set;}
    }
}
