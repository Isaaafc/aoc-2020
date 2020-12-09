// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Collections.Generic
open System.Text.RegularExpressions

let data = "../../../data/day_7_test.txt"

let readLines filePath = System.IO.File.ReadAllLines(filePath) |> Seq.toList

let rec search (graph: Dictionary<string, string []>) key head (visited: string list) = 
    if graph.[head].Length = 0 || List.contains head visited then
        0
    elif Array.contains head graph.[head] then
        1
    else
        let hasKey =
            seq {
                for h in graph do
                    search graph key h.Key (head :: visited)
            } |> Seq.reduce(( + ))
        
        if hasKey > 0 then 1 else 0

let ptn = Regex(@"(\d+ )?([a-z\s]+) bag")

let q1 (lines: string list) =
    let allColors =
        seq {
            for l in lines do
                let key = (l.Split " contain ").[0]
                ptn.Match(key).Groups.[2].Value
        } |> Seq.toList

    let graph =
        seq {
            for l in lines do
                let s = l.Split " contain "

                let key = ptn.Match(s.[0]).Groups.[2].Value
                let values =
                    seq {
                        let notNone = fun e -> not (Seq.contains "no other bags" e)
                        
                        for b in Array.filter notNone (s.[1].Split ",")do
                            ptn.Match(b.Trim()).Groups.[2].Value
                    } |> Seq.toArray
                
                key, values
        } |> Seq.toList
        |> dict
    
    graph

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%A" (q1 lines)
    0 // return an integer exit code