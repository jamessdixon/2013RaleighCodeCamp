namespace Tff.Ransom.FS

open System
 
type dollar =
    {Id: int;
     SerialNumber: string;
     federalReserveDistrict: int;
     seriesDate: int;
     signature: string}
 
type dollarProvider() =
    let randomNumberGenerator = new System.Random()
    let createSerialNumber =
        List.init 9 (fun _ -> randomNumberGenerator.Next(0,9))
                                    |> Seq.map string
                                    |> String.concat ""
    let createDollar id =
        let returnDollar = {Id=id;
            SerialNumber= createSerialNumber ;
            federalReserveDistrict=randomNumberGenerator.Next(0,13);
            seriesDate=2000+randomNumberGenerator.Next(0,9);
            signature=""}
        returnDollar
 
    member this.GetDollars (numberOfDollars:int) =
        List.init numberOfDollars (fun index -> createDollar index)
