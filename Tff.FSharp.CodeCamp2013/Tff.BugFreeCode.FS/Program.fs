open System
open Tff.BugFreeCode.CS
 
let claim0 = Claim(1, 1, 1, DateTime.Now, 100.00)
let claim1 = Claim(2, 1, 2, DateTime.Now, 201.00)
let claim2 = Claim(3, 1, 3, DateTime.Now, 350.00)
let claim3 = Claim(4, 1, 4, DateTime.Now, 600.00)
 
let allClaims = [claim0; claim1; claim2; claim3]
 
let goodClaims =
    allClaims
    |> List.filter(fun claim -> claim.IsASuspectClaim = false)   
 
 
[<EntryPoint>]
let main argv =
    Console.WriteLine(String.Format("{0} Claims in allClaims",List.length(allClaims)))
    Console.WriteLine(String.Format("{0} Claims in goodClaims",List.length(goodClaims)))
 
    let userInput = Console.ReadKey()
    0
