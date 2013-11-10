#r @"C:\TFS\Tff.RDotNetExample_Solution\packages\R.NET.1.5.3\lib\net40\RDotNet.dll"
#r @"C:\TFS\Tff.RDotNetExample_Solution\packages\R.NET.1.5.3\lib\net40\RDotNet.NativeLibrary.dll"

open System.IO
open RDotNet


//open R
let environmentPath = System.Environment.GetEnvironmentVariable("PATH")
let binaryPath = @"C:\Program Files\R\R-3.0.1\bin\x64"
System.Environment.SetEnvironmentVariable("PATH",environmentPath+System.IO.Path.PathSeparator.ToString()+binaryPath)

let engine = RDotNet.REngine.CreateInstance("RDotNet")
engine.Initialize()


//open dataset
let path = @"C:\TFS\Tff.CodeCamp2013\Tff.RExample\ufo_awesome.txt"
let fileStream = new FileStream(path,FileMode.Open,FileAccess.Read)
let streamReader = new StreamReader(fileStream)
let contents = streamReader.ReadToEnd()
let usStates = [|"AL";"AK";"AZ";"AR";"CA";"CO";"CT";"DE";"DC";"FL";"GA";"HI";"ID";"IL";"IN";"IA";
                    "KS";"KY";"LA";"ME";"MD";"MA";"MI";"MN";"MS";"MO";"MT";"NE";"NV";"NH";"NJ";"NM";
                    "NY";"NC";"ND";"OH";"OK";"OR";"PA";"RI";"SC";"SD";"TN";"TX";"UT";"VT";"VA";"WA";
                    "WV";"WI";"WY"|]
let cleanContents =
    contents.Split([|'\n'|])
    |> Seq.map(fun line -> line.Split([|'\t'|]))
    |> Seq.filter(fun values -> values |> Seq.length = 6)
    |> Seq.filter(fun values -> values.[0].Length = 8)
    |> Seq.filter(fun values -> values.[1].Length = 8)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[0].Substring(0,4)) > 1900)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[1].Substring(0,4)) > 1900)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[0].Substring(0,4)) < 2100)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[1].Substring(0,4)) < 2100)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[0].Substring(4,2)) > 0)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[1].Substring(4,2)) > 0)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[0].Substring(4,2)) <= 12)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[1].Substring(4,2)) <= 12)      
    |> Seq.filter(fun values -> System.Int32.Parse(values.[0].Substring(6,2)) > 0)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[1].Substring(6,2)) > 0)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[0].Substring(6,2)) <= 31)
    |> Seq.filter(fun values -> System.Int32.Parse(values.[1].Substring(6,2)) <= 31)
    |> Seq.filter(fun values -> values.[2].Split(',').[1].Trim().Length = 2)
    |> Seq.filter(fun values -> Seq.exists(fun elem -> elem = values.[2].Split(',').[1].Trim().ToUpperInvariant()) usStates)
    |> Seq.map(fun values -> 
        System.DateTime.ParseExact(values.[0],"yyyymmdd",System.Globalization.CultureInfo.InvariantCulture),
        System.DateTime.ParseExact(values.[1],"yyyymmdd",System.Globalization.CultureInfo.InvariantCulture),
        values.[2].Split(',').[0].Trim(),
        values.[2].Split(',').[1].Trim().ToUpperInvariant(),
        values.[3],
        values.[4],
        values.[5])
cleanContents

let relevantContents =
    cleanContents
    |> Seq.map(fun (a,b,c,d,e,f,g) -> a.Year,d,g.Length)


let reportLength = engine.CreateIntegerVector(relevantContents |> Seq.map (fun (a,b,c) -> c))
engine.SetSymbol("reportLength", reportLength)
let year = engine.CreateIntegerVector(relevantContents |> Seq.map (fun (a,b,c) -> a))
engine.SetSymbol("year", year)
let state = engine.CreateCharacterVector(relevantContents |> Seq.map (fun (a,b,c) -> b))
engine.SetSymbol("state", state)

let calcExpression = "lm(formula = reportLength ~ year + state)"
let testResult = engine.Evaluate(calcExpression).AsList()

