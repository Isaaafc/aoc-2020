// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Microsoft.FSharp.Core

let data = "../../../data/day_6.txt"

let readLines filePath = System.IO.File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine) |> Array.map(fun x -> x.Replace(Environment.NewLine, " ").Trim())

let q1 (lines: string []) = 
    let cnt =
        seq {
            for line in lines do
                (Set.unionMany (seq {for ans in line.Split " " do Set.ofSeq ans})).Count
        } |> Seq.reduce(( + ))
    
    cnt

let q2 (lines: string[]) =
    let cnt =
        seq {
            for line in lines do
                (Set.intersectMany (seq {for ans in line.Split " " do Set.ofSeq ans})).Count
        } |> Seq.reduce(( + ))
    
    cnt

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code