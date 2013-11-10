module Tff.EulerQuestion1.FS

open System
 
let tryOne = [1..1000]
                    |> Seq.filter(fun number -> (number%3 = 0 || number%5 = 0))
                    |> Seq.sum
                    