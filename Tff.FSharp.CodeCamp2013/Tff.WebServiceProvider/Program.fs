open System
open System.ServiceModel
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type TerraService = WsdlService<"http://msrmaps.com/TerraService2.asmx?WSDL">

[<EntryPoint>]
let main argv = 
    let client = TerraService.GetTerraServiceSoap()
    let place = new TerraService.ServiceTypes.msrmaps.com.Place(City="Hartford", State="CT", Country="United States")
    let location = client.ConvertPlaceToLonLatPt(place);
    printfn "Hartford Latitude %f Longitude %f" location.Lat location.Lon
    let result = Console.ReadKey()
    0 