
---
**IMPORTANT:**

**This C# SDK has been deprecated and replaced by the following C# SDK:**

https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp-openapi

**Please see the new SDK. The SDK on this page is deprecated. For current users, this SDK will continue to work but will not include updated features.**

---

# Pitney Bowes Shipping API C# Client [DEPRECATED]

**This C# SDK has been deprecated and replaced by the following C# SDK:**

https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp-openapi

# Legacy Documentation

**Latest version 1.3.0 is not backward compatible with older versions as Surcharge Enum from Rates object has been removed and now it's a string**

This package provides an easy to use, developer centric .net interface to the Pitney Bowes Shipping API.
The package uses the [.NET Standard 2.0 framework](https://docs.microsoft.com/en-us/dotnet/standard/net-standard), meaning 
it is compatible with:
* .NET core 2.0 and above
* .NET framework 4.6.1 and above

Mono and Xamarin are also supported.

## Features:
* **full wrapping of the API**. Hides (encapsulates) all protocol details, including authentication and report pagination.
* **strong typing** objects for all entities and enums for all options with intellisense support.
* **Contract via interfaces** as well as DTOs to reduce the need to copy data
* **Linq provider** for reports
* **Fluent interface** - less typing and really good way to extend with extension methods
* **Support for the metadata** provided by the carrier rules method. Use this for local validation/UI options/rate shop.
* **Mocking and recording** of live messages to disk for capture or later playback in mock mode. Mocking for unit and regression testing.
* **Example** console app.
* **Plug in your own configuration and log providers**
* **Keeps the API secret out of cleartext**

## Prerequisites

- To generate shipping labels, you will need a Pitney Bowes Shipping API sandbox account. The account is free and gives access to a fully functional sandbox environment. Sign up for the account here: [Shipping API Signup](https://signup.pitneybowes.com/signup/shipping).

  You will need the following information from your Shipping API account. To get the information, see [Getting Started](https://shipping.pitneybowes.com/getting-started.html).

This project has the code for a NuGet package that provides a fluent C# interface to the Pitney Bowes Shipping API.
The package uses the [.NET Standard 2.0 framework](https://docs.microsoft.com/en-us/dotnet/standard/net-standard), meaning 
it is compatible with:
* .NET core 2.0
* .NET framework 4.6.1 and above

Mono and Xamarin are also supported.

## Features:
* **full wrapping of the API**. Hides (encapsulates) all protocol details, including authentication and report pagination..
* **strong typing** – objects for all entities. Enums for all options – which really helps in visual studio intellisense.
* **Contract via interfaces** as well as DTOs to reduce the need to copy data
* **Linq provider** for reports
* **Fluent interface** - less typing and really good way to extend with extension methods
* **Support for the metadata** provided by the carrier rules method. Use this for local validation/UI options/rate shop.
* **Mocking and recording** of live messages to disk for capture or later playback in mock mode. Mocking for unit and regression testing.
* **Example** console app.
* **Plug in your own configuration and log providers**
* **Keeps the API secret out of cleartext**

Latest changes - [CHANGES](CHANGES.md)


## Prerequisites

- To use the C# client to generate shipping labels, you will need a Pitney Bowes Shipping API sandbox account. The account is free and gives access to a fully functional sandbox environment. Sign up for the account here: [Shipping API Signup](https://signup.pitneybowes.com/signup/shipping).

  You will need the following information from your Shipping API account. To get the information, see [Getting Started](https://shipping.pitneybowes.com/getting-started.html).

   * Your API key
   * API secret
   * Developer ID
   * Shipper ID

- VSCode or Visual Studio. The Community Edition of Visual Studio is fine.

- dotnet core 2.0 or later

- git command line (required if [building the system out-of-box](https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp#building-the-system-out-of-box))

## Getting Started
   You can get started by doing either of the following:

   - [Using the NuGet Package](https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp#using-the-nuget-package)
   - [Building the System Out-of-Box](https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp#building-the-system-out-of-box)

## Using the NuGet Package
If you just want to use the solution without building it, download it from [on nuget.org](https://www.nuget.org/packages/shippingapi/).

If you are developing on Windows, I'd recommend that you install [Telerik Fiddler](http://www.telerik.com/fiddler), which will let you see the messages with the Pitney Bowes servers.

1. Build an example app:
   ```bash
   $ mkdir myshippingprojdir
   $ cd myshippingprojdir
   $ dotnet new console
   $ dotnet add package ShippingAPI
   ```

2. In Visual Studio create a new console app, and then in the package manager console
   ```
   PM> install-package ShippingAPI
   ```

3. Replace your Program.cs file with one of the following from the
   [provided examples](https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp/tree/master/examples):

   - [The MyShip.cs sample file](https://raw.githubusercontent.com/PitneyBowes/pitneybowes-shipping-api-csharp/master/examples/MyShip/MyShip.cs).
     This builds an app that generates a label. The remainder of these steps
     explain how to configure this app.

   - [The Program.cs sample file](https://raw.githubusercontent.com/PitneyBowes/pitneybowes-shipping-api-csharp/master/examples/example/Program.cs).
     This builds an app that that integrates your logging and configuration
     systems. This also uses all the API's methods. If you use this file, plug
     in your logging and configuration information appropriately and skip the
     rest of these steps.

4. Add your own IDs. IDs are case-sensitive. Do one of the following:

   - Replace the values in the code below:
     ```csharp

     sandbox.AddConfigItem("ApiKey", "your api key");
     sandbox.AddConfigItem("ApiSecret", "your api secret");
     sandbox.AddConfigItem("ShipperID", "your shipper id");
     sandbox.AddConfigItem("DeveloperID", "your developer id");
     ```
   - Or create a shippingapisettings.json file in `%APPDATA%` on Windows, or ~ on OSX and Linux:
     ```json
     { 
         "ApiKey": "!###",
         "ApiSecret": "###",
         "ShipperID": "1234567890",
         "DeveloperID": "1234567890" 
     }
     ```

5. To create a shipping label:
   ```csharp
   var shipment = ShipmentFluent<Shipment>.Create()
       .ToAddress((Address)AddressFluent<Address>.Create()
           .AddressLines("643 Greenway Rd")
           .PostalCode("28607")
           .CountryCode("US")
           .Verify())
      .FromAddress((Address)AddressFluent<Address>.Create()
           .Company("Pitney Bowes Inc")
           .AddressLines("27 Waterview Drive")
           .CityTown("Shelton").StateProvince("CT").PostalCode("06484")
           .CountryCode("US")
           )
      .Parcel((Parcel)ParcelFluent<Parcel>.Create()
           .Dimension(12, 12, 10)
           .Weight(16m, UnitOfWeight.OZ))
      .Rates(RatesArrayFluent<Rates>.Create()
           .USPSPriority<Rates, Parameter>())
      .Documents((List<IDocument>)DocumentsArrayFluent<Document>.Create()
           .ShippingLabel(ContentType.URL, Size.DOC_4X6, FileFormat.PDF))
      .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
           .ShipperId("your shipper id")    // ******* dont forget this one too *******
           )
      .TransactionId(Guid.NewGuid().ToString().Substring(15));

   var label = Api.CreateShipment((Shipment)shipment).GetAwaiter().GetResult();
   if (label.Success)
   {
       var sw = new StreamWriter("label.pdf");
       foreach (var d in label.APIResponse.Documents)
       {
           Api.WriteToStream(d, sw.BaseStream).GetAwaiter().GetResult();
       }
   }
   ```

#### Support

If you encounter problems, please contact Support at <ShippingAPISupport@pb.com>.

## Building the System Out-of-Box

The following commands are specific to Windows. The code also builds on macOS Sierra using Visual Studio Community edition and VSCode.

At the Visual Studio developer command prompt:
 
 ```
	C:\Development>mkdir apitest
 
	C:\Development>cd apitest
 
	C:\Development\apitest>git clone https://github.com/PitneyBowes/pitneybowes-shipping-api-csharp.git
	Cloning into 'pitneybowes-shipping-api-csharp'...
	remote: Counting objects: 665, done.
	remote: Total 665 (delta 0), reused 0 (delta 0), pack-reused 665
	Receiving objects: 100% (665/665), 1.49 MiB | 352.00 KiB/s, done.
	Resolving deltas: 100% (505/505), done.
 
	C:\Development\apitest\shippingAPI> cd pitneybowes-shipping-api-csharp
 
	C:\Development\apitest\shippingAPI> dotnet restore
 
	C:\Development\apitest\shippingAPI> dotnet build
 
	C:\Development\apitest\shippingAPI> dotnet publish
 
	C:\Development\apitest\shippingAPI> cd examples\example.core\bin\Debug\netcoreapp2.0\publish
 
	C:\Development\apitest\examples\example.core\bin\Debug\netcoreapp2.0\publish> dotnet example.dll
	9405509898642004103722
	Document written to C:\Users\patrick\AppData\Local\Temp\2\USPS2200080642743578.PDF
	Document written to C:\Users\patrick\AppData\Local\Temp\2\9475709899581000234042.PDF
```

## Running the tests

The tests use [xunit](https://xunit.github.io/). The API can run in either live or mocked mode. When it is in mocked mode the response to API requests are 
read from a file. The file is identified by certain attributes of the response - transaction ID in the case of a shipment, so different test
scenarios can be set up using the mocked interface. 

Tests can also be run from sample messages - the message is deserialized from a file and an API request is created and sent. See the files in the testData folder for examples.
## Built With

* [NewtonSoft.JSON](http://www.dropwizard.io/1.0.2/docs/) 
* [xUnit](https://xunit.github.io/)

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Patrick Farry** - *Initial work* - [Pitney Bowes]

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

Copyright 2016 Pitney Bowes Inc.
Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License.  You may obtain a copy of the License in the README file or at
    https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License for the specific language governing permissions and limitations under the License.
 - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* NewtonSoft.JSON
