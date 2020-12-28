// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Text.RegularExpressions
open System.Collections.Generic

let data = "../../../data/day_16.txt"

let toInt (s: string) = (s.Split ",") |> Array.map(Int32.Parse)

let parseFields (s:string) =
    let ptn = @"(?<field>[a-z\s]+): (?<llmt_1>\d{1,3})-(?<ulmt_1>\d{1,3}) or (?<llmt_2>\d{1,3})-(?<ulmt_2>\d{1,3})"
    let s = s.Split "\n"

    let fields =
        seq {
            for field in s do
                let m = Regex.Match(field, ptn)
                let lmt1 = Int32.Parse m.Groups.["llmt_1"].Value, Int32.Parse m.Groups.["ulmt_1"].Value
                let lmt2 = Int32.Parse m.Groups.["llmt_2"].Value, Int32.Parse m.Groups.["ulmt_2"].Value

                m.Groups.["field"].Value, (lmt1, lmt2)
        } |> dict

    let validSet =
        seq {
            for field in s do
                let m = Regex.Match(field, ptn)
                List.append [Int32.Parse m.Groups.["llmt_1"].Value..Int32.Parse m.Groups.["ulmt_1"].Value] [Int32.Parse m.Groups.["llmt_2"].Value..Int32.Parse m.Groups.["ulmt_2"].Value]
        } |> Seq.reduce(List.append)
        |> Set.ofList
        
    fields, validSet 

let readLines filePath = 

    let s = System.IO.File.ReadAllText(filePath).Split("\n\n")
    
    let fields, validSet = parseFields s.[0]

    let ticket = toInt (s.[1].Split "\n").[1]
    let nearby =
        let nts = s.[2].Split "\n"
        nts.[1..nts.Length - 2] |> Array.map(toInt)
    
    fields, validSet, ticket, nearby

let q1 nearby (validSet: int Set) =
    let error =
        seq {
            for nt in nearby do
                for i in nt do
                    if validSet.Contains i then 0 else i
        } |> Seq.reduce(( + ))
    
    error

let isValid (ticket: int array) (validSet: int Set) = 
    seq {
        for i in ticket do
            validSet.Contains i
    } |> Seq.reduce(( && ))

let inRange (i: int) (lmt1: Tuple<int, int>) (lmt2: Tuple<int, int>) =
    (i >= fst lmt1 && i <= snd lmt1) || (i >= fst lmt2 && i <= snd lmt2)

let q2 (fields: IDictionary<string, Tuple<Tuple<int, int>, Tuple<int, int>>>) (validSet: int Set) (ticket: int array) (nearby: int array array) =
    let validTickets = nearby |> Array.filter(fun el -> isValid el validSet)
    let validFields = 
        seq {
            for _ in [0..ticket.Length - 1] do
                fields.Keys |> Seq.toList
        } |> Seq.toList

    let rec filterValidFields (nearby: int array array) (prevValidFields: string list list) =
        if nearby.Length = 0 then
            prevValidFields
        else            
            let prevValidFields = 
                let nt = nearby.[0]

                seq {
                    for i in [0..nt.Length - 1] do
                        prevValidFields.[i] |> List.filter(fun el -> inRange nt.[i] (fst fields.[el]) (snd fields.[el]))
                } |> Seq.toList

            filterValidFields nearby.[1..] prevValidFields

    let validFields = filterValidFields validTickets validFields

    let isTaken (validFields: string list list) = 
        validFields
        |> List.filter(fun el -> el.Length = 1)
        |> List.map(fun el -> el.[0])
        |> Set.ofList

    let rec filterTakenFields (taken: string Set) (ticket: int array) (prevValidFields: string list list) =
        // printfn "Taken count: %d" taken.Count
        
        if taken.Count = ticket.Length then
            prevValidFields
        else
            let prevValidFields =
                seq {
                    for i in [0..prevValidFields.Length - 1] do
                        if prevValidFields.[i].Length = 1 then
                            prevValidFields.[i]
                        else
                            prevValidFields.[i] |> List.filter(taken.Contains >> not)
                } |> Seq.toList

            filterTakenFields (isTaken prevValidFields) ticket prevValidFields
    
    let taken = isTaken validFields

    let takenFields = filterTakenFields taken ticket validFields |> List.map(fun el -> el.[0])

    let mul = 
        seq {
            for i in [0..takenFields.Length - 1] do
                let f = takenFields.[i]

                if f.Contains "departure" then
                    int64(ticket.[i])
                else
                    int64(1)
        } |> Seq.reduce(( * ))
    
    mul
    
[<EntryPoint>]
let main argv =
    let fields, validSet, ticket, nearby = readLines data
    printfn "%d" (q1 nearby validSet)
    printfn "%d" (q2 fields validSet ticket nearby)
    0 // return an integer exit code