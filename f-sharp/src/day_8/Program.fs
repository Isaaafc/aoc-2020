// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

let data = "../../../data/day_8.txt"

let readLines filePath = 
    seq {
        for line in System.IO.File.ReadAllLines(filePath) do
            let s = line.Trim().Split " "
            s.[0], (s.[1] |> Int32.Parse)
    } |> Seq.toList

let rec runProg (lines: Tuple<string, int> list) i (visited: int list) (acc: int) = 
    if List.contains i visited then
        acc, true
    elif i >= lines.Length then
        acc, false
    else
        let visited = i :: visited
        let key, arg = lines.[i]
        
        match key with
        | "acc" -> runProg lines (i + 1) visited (acc + arg)
        | "jmp" -> runProg lines (i + arg) visited acc
        | _ -> runProg lines (i + 1) visited acc

let rec runUntil (lines: Tuple<string, int> list) (updateQueue: Tuple<int, string> list) (acc: int) infinite =
    if (not infinite) || updateQueue.Length = 0 then
        acc
    else
        let idx, key = updateQueue.[0]

        let linesCopy = 
            seq {
                for i in [0..lines.Length - 1] do
                    let k, arg = lines.[i]

                    if i = idx then
                        key, arg
                    else
                        k, arg
            } |> Seq.toList
        
        let acc, infinite = runProg linesCopy 0 [] 0

        runUntil lines updateQueue.[1..] acc infinite


let q1 lines =
    let acc, _ = runProg lines 0 [] 0
    acc

let q2 (lines: Tuple<string, int> list) =
    let updateQueue =
            seq {
                for i in [0..lines.Length - 1] do
                    let key, _ = lines.[i]

                    match key with
                    | "jmp" -> i, "nop"
                    | "nop" -> i, "jmp"
                    | _ -> i, key
            } |> Seq.filter (fun (i, key) -> key <> "acc")
            |> Seq.toList
    
    runUntil lines updateQueue 0 true

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code