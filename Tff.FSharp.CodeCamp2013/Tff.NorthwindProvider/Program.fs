open System
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

type schema = SqlDataConnection<"Data Source=.;Initial Catalog=Northwind;Integrated Security=SSPI;">
let context = schema.GetDataContext()

[<EntryPoint>]
let main argv = 
    let customerQuery =
        query {
            for row in context.Customers do
            select row
        }
    customerQuery |> Seq.iter (fun row -> Console.WriteLine(String.Format("{0} {1}",row.ContactTitle, row.ContactName)))

    let result = Console.ReadKey()
    0
