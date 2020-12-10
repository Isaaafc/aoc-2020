// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Collections.Generic

let data = "../../../data/day_10.txt"

let readLines filePath = System.IO.File.ReadAllLines(filePath) |> Seq.map(Int64.Parse) |> Seq.toList

let chain (lines: int64 list) =
    let lines = int64(0)::((lines.[lines.Length - 1] + int64(3))::lines) |> List.sort
    let count1 = 
        seq {
            for i in [1..lines.Length - 1] do
                if lines.[i] - lines.[i - 1] = int64(1) then
                    1
                else
                    0
        } |> Seq.sum
    
    let count3 =
        seq {
            for i in [1..lines.Length - 1] do
                if lines.[i] - lines.[i - 1] = int64(3) then
                    1
                else
                    0
        } |> Seq.sum

    count1, count3

let dp lines =
    let lines = int64(0)::lines |> List.sort
    let count = new Dictionary<int, int64>()

    count.Add(lines.Length - 1, int64(1))

    for i = lines.Length - 2 downto 0 do
        let cnt =
            seq {
                for j = i + 1 to lines.Length - 1 do
                    if List.contains (lines.[j] - lines.[i]) (List.map (int64) [1;2;3]) then
                        count.[j]
                    else
                        int64(0)
            } |> Seq.sum
        
        count.Add(i, cnt)

    count.[0]

let q1 (lines: int64 list) =
    let count1, count3 = chain lines
    count1 * count3

let q2 (lines: int64 list) =
    dp lines

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%A" (q2 lines)
    0 // return an integer exit code