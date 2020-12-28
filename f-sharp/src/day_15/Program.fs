// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
let data = "../../../data/day_15.txt"

let readLines filePath = 
    System.IO.File.ReadAllLines(filePath) 
    |> Seq.map(fun x -> (x.[0], Int32.Parse x.[1..]))
    |> Seq.toList

let runLoop (line: string) stop = 
    let nums = line.Split "," |> Array.map(Int32.Parse) |> Array.toList
    let arr = seq {for _ in [0..stop] do -1} |> Seq.toArray

    for i in [0..nums.Length - 2] do
        arr.[nums.[i]] <- i + 1
    
    let lastNums = [| nums.[nums.Length - 1] |]

    for turn in [nums.Length..stop] do
        let lastNum = lastNums.[lastNums.Length - 1]
        let newLastNum = if arr.[lastNum] < 0 then 0 else turn - arr.[lastNum]

        arr.[lastNum] <- turn
        lastNums.[lastNums.Length - 1] <- newLastNum
    
    lastNums.[lastNums.Length - 1]

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (runLoop lines)
    printfn "%d" (runLoop "0,3,6" 2020)
    0 // return an integer exit code