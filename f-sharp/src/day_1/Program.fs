// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let data = "../../../data/day_1_1.txt"

let readLines filePath = seq {for l in System.IO.File.ReadAllLines(filePath) do Int32.Parse l} |> Seq.toList

let q1 =
    let lines = readLines data

    let res = 
        seq {
            for i = 0 to lines.Length - 1 do
                for j = i + 1 to lines.Length - 1 do
                    if lines.[i] + lines.[j] = 2020 then lines.[i] * lines.[j] else 0
        } |> Seq.toList
        |> List.sum

    res

let q2 = 
    let lines = readLines data

    let res = 
        seq {
            for i = 0 to lines.Length - 1 do
                for j = i + 1 to lines.Length - 1 do
                    for k = j + 1 to lines.Length - 1 do
                        if lines.[i] + lines.[j] + lines.[k] = 2020 then lines.[i] * lines.[j] * lines.[k] else 0
        } |> Seq.toList
        |> List.sum

    res

[<EntryPoint>]
let main argv =
    printfn "%d" q1
    printfn "%A" q2
    0