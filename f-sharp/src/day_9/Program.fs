// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

let data = "../../../data/day_9.txt"

let readLines filePath = System.IO.File.ReadAllLines(filePath) |> Seq.map(Int64.Parse) |> Seq.toList

let isValid n (preamble: int64 list) =
    seq {
        for i in [0..preamble.Length - 2] do
            for j in [i + 1..preamble.Length - 1] do
                (preamble.[i] <> preamble.[j]) && (preamble.[i] + preamble.[j] = n)
    } |> Seq.reduce(( || ))

let search (lines: int64 list) nPreamble =
    seq {
        for i in [nPreamble..lines.Length - 1] do
            let preamble = lines.[i - nPreamble..i - 1]
            let valid = isValid lines.[i] preamble

            if not valid then
                lines.[i]
            else
                int64 0
    } |> Seq.filter (fun x -> x > int64 0)
    |> Seq.head

let rec findContiguousSum (lines: int64 list) n l r =
    if r > l && r <= lines.Length then
        let window = lines.[l..r - 1]
        let sumWindow = List.sum window

        if sumWindow < n then
            findContiguousSum lines n l (r + 1)
        elif sumWindow > n then
            findContiguousSum lines n (l + 1) r
        else
            window
    else
        []

let q1 (lines: int64 list) =
    search lines 25

let q2 (lines: int64 list) =
    let firstInvalid = search lines 25
    let window = findContiguousSum lines firstInvalid 0 1 |> List.sort

    window.[0] + window.[window.Length - 1]

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code