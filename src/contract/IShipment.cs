﻿/*
Copyright 2018 Pitney Bowes Inc.

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

using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Object to create a shipment and purchases a shipment label. The API returns the label as either a Base64 string or a link to a PDF.
    /// </summary>
    public interface IShipment
    {
        /// <summary>
        /// REQUIRED. A unique identifier for each transaction that cannot exceed 25 characters.
        /// </summary>
        string TransactionId { get; set; }
        /// <summary>
        /// MinimalAddressValidation header option
        /// </summary>
        string MinimalAddressValidation { get; set; }
        /// <summary>
        /// Shipper rate plan, if available.
        /// Important: Do not include this header if creating a scan-based return (SBR) label.
        /// </summary>
        string ShipperRatePlan { get; set; }
        /// <summary>
        /// Required for PB Presort. The job number that represents the agreement between the merchant and PB Presort. 
        /// This was provided by Pitney Bowes during merchant onboarding for PB Presort.
        /// </summary>
        string ShipmentGroupId { get; set; }
        /// <summary>
        /// Required for PB Presort. The merchant’s Mailer ID (MID), as provided by Pitney Bowes during merchant 
        /// onboarding for PB Presort.
        /// </summary>
        string IntegratorCarrierId { get; set; }
        /// <summary>
        /// Request devlivery commitment
        /// </summary>
        bool IncludeDeliveryCommitment { get; set; }
        /// <summary>
        /// REQUIRED. Origin address. See Create a Shipment for considerations when specifying multiple address lines when using 
        /// MINIMAL_ADDRESS_VALIDATION.
        /// </summary>
        IAddress FromAddress { get; set; }
        /// <summary>
        /// REQUIRED.Destination address.
        /// Note: You can specify multiple address lines in the shipment’s destination address.See address object for information on how 
        /// the API processes multiple address lines.
        /// </summary>
        IAddress ToAddress { get; set; }
        /// <summary>
        /// INTERNATIONAL SHIPMENTS ONLY. Required if the return shipment is not going to the fromAddress but is instead to an alternate 
        /// return address.
        /// </summary>
        IAddress AltReturnAddress { get; set; }
        /// <summary>
        /// REQUIRED. Contains physical characteristics of the parcel.
        /// </summary>
        IParcel Parcel { get; set; }
        /// <summary>
        /// REQUIRED. Information related to the shipment rates.
        /// </summary>
        IEnumerable<IRates> Rates { get; set; }
        /// <summary>
        /// Add Rates object to the Rates IEnumerable
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        IRates AddRates(IRates r);
        /// <summary>
        /// A list of shipment documents pertaining to a shipment, including the label.
        /// </summary>
        IEnumerable<IDocument> Documents { get; set; }
        /// <summary>
        /// Add a Document to the Documents IEnumerable
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        IDocument AddDocument(IDocument d);
        /// <summary>
        /// Each object in this array defines a shipment option. The available options depend on the carrier, origin country, and destination country.
        /// If you are creating a shipment, this array is required and must contain the SHIPPER_ID option.
        /// </summary>
        IEnumerable<IShipmentOptions> ShipmentOptions { get; set; }
        /// <summary>
        /// Add option to the ShipmentOptions IEnumerable
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        IShipmentOptions AddShipmentOptions(IShipmentOptions o);
        /// <summary>
        /// ONLY FOR: international, APO/FPO/DPO, territories/possessions, and FAS shipments. Customs related information.
        /// </summary>
        ICustoms Customs { get; set; }
        /// <summary>
        /// SBR LABELS ONLY.
        ///
        /// If you are creating a scan-based return (SBR) label, set this to RETURN.
        /// </summary>
        ShipmentType ShipmentType { get; set; }
        /// <summary>
        /// Newgistics Only. This array maps client-generated identifiers to fields in the Newgistics package record. 
        /// The information in this array does not appear on the shipping label. The array takes up to three objects, 
        /// and each object maps an identifier to a specific Newgistics field.An object’s sequence in the array 
        /// determines which Newgistics field the object maps to.The first object in the array maps to the Newgistics 
        /// “ReferenceNumber” field; the second to the “AddlRef1” field; and the third to the “AddlRef2” field.
        /// </summary>
        /// <value>The references.</value>
        IEnumerable<IReference> References {get;set;}
        /// <summary>
        /// Unique identifier for the shipment, generated by the system in response to shipment purchase.
        /// </summary>
        string ShipmentId { get; set; }
        /// <summary>
        /// Tracking number assigned to the shipment by the system.
        /// </summary>
        string ParcelTrackingNumber { get; set; }
    }
}