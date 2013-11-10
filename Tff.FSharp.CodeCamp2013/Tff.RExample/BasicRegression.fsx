#r @"C:\TFS\Tff.RDotNetExample_Solution\packages\R.NET.1.5.3\lib\net40\RDotNet.dll"
#r @"C:\TFS\Tff.RDotNetExample_Solution\packages\R.NET.1.5.3\lib\net40\RDotNet.NativeLibrary.dll"

open System.IO
open RDotNet

let environmentPath = System.Environment.GetEnvironmentVariable("PATH")
let binaryPath = @"C:\Program Files\R\R-3.0.1\bin\x64"
System.Environment.SetEnvironmentVariable("PATH",environmentPath+System.IO.Path.PathSeparator.ToString()+binaryPath)

let engine = RDotNet.REngine.CreateInstance("RDotNet")
engine.Initialize()

let year = engine.CreateIntegerVector([2000;2001;2002;2003;2004])
engine.SetSymbol("year", year)
let rate = engine.CreateNumericVector([9.34;8.50;7.62;6.93;6.60])
engine.SetSymbol("rate", rate)
let calcExpression = "lm(formula=rate~year)"
let testResult = engine.Evaluate(calcExpression).AsList()

//testResult.Item(0).AsNumeric()

//val it : NumericVector = seq [1419.208; -0.705]
//val it : NumericVector = seq [0.132; -0.003; -0.178; -0.163; ...]
//val it : NumericVector = seq [-17.43685809; -2.22940575; -0.2264262472; -0.2237815612; ...]
//val it : NumericVector = seq [2.0]
//val it : NumericVector = seq [9.208; 8.503; 7.798; 7.093; ...]
//val it : NumericVector = seq [0.0; 1.0]
//OOM
//val it : NumericVector = seq [3.0]

