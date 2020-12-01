// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO

let data = @"C:\Users\isaac\Documents\Projects\aoc-2020\data\day_1_1.txt"

let readLines filePath = seq {for l in System.IO.File.ReadAllLines(filePath) do Int32.Parse l} |> Seq.toArray

let q1 =
    let lines = readLines data
    let res = -1

    for i = 0 to lines.Length - 1 do
        for j = i + 1 to lines.Length - 1 do
            let s = lines.[i] + lines.[j]
            res = if s = 2020 && res = -1 then lines.[i] * lines.[j] else res
            printfn "%d" res

    res

[<EntryPoint>]
let main argv =
    printfn "Hello world %d" q1
    0 // return an integer exit code