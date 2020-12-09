// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open System.Text.RegularExpressions

let data = "../../../data/day_2.txt"

let readLines filePath = System.IO.File.ReadAllLines(filePath) |> Seq.toList

let parseLine line =
    let m = Regex.Match(line, @"(\d+)\-(\d+) ([a-z]): (.+)$")
    (m.Groups.[1].Value |> Int32.Parse, m.Groups.[2].Value |> Int32.Parse, m.Groups.[3].Value.[0], m.Groups.[4].Value)
    
let q1 =
    let lines = readLines data
    let pws = seq {for l in lines do parseLine l} |> Seq.toList
    let cnt = 
        seq {
            for l, r, c, pw in pws do
                let cCnt = pw |> Seq.toList |> List.map(fun x -> if x = c then 1 else 0) |> List.sum
                if cCnt >= l && cCnt <= r then 1 else 0
        } |> Seq.toList
        |> List.sum

    cnt

let q2 =
    let lines = readLines data
    let pws = seq {for l in lines do parseLine l} |> Seq.toList
    let cnt = 
        seq {
            for l, r, c, pw in pws do
                let pwSeq = pw |> Seq.toList
                let isLeft = if pwSeq.[l - 1] = c then 1 else 0
                let isRight = if pwSeq.[r - 1] = c then 1 else 0

                if isLeft + isRight = 1 then 1 else 0
        } |> Seq.toList
        |> List.sum

    cnt

[<EntryPoint>]
let main argv =
    printfn "%d" q1
    printfn "%d" q2
    0