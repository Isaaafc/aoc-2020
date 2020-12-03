// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

let data = "../../../data/day_3.txt"

let readLines filePath = System.IO.File.ReadAllLines(filePath) |> Seq.toList |> List.map(Seq.toList)

let isTree (lines: _ list list) i j =
    if lines.[j].[i] = '#' then 1 else 0

let rec traverse (lines: _ list list) i j cnt slopeI slopeJ =
    if j < lines.Length - slopeJ then
        let i = (i + slopeI) % lines.[0].Length
        let j = j + slopeJ

        traverse lines i j (cnt + (isTree lines i j)) slopeI slopeJ
    else
        cnt

let lines = readLines data

let q1 lines = 
    traverse lines 0 0 0 3 1

let q2 lines =
    let slopes = [(1, 1); (3, 1); (5, 1); (7, 1); (1, 2)]
    let mul =
        seq {
            for slope in slopes do
                let (slopeI, slopeJ) = slope
                yield traverse lines 0 0 0 slopeI slopeJ
        } |> Seq.map(fun x -> int64(x))
        |> Seq.reduce(fun x y -> x * y)

    mul
    
[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code