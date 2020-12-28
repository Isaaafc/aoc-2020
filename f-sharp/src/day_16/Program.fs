// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Text.RegularExpressions

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

[<EntryPoint>]
let main argv =
    let fields, validSet, ticket, nearby = readLines data
    printfn "%d" (q1 nearby validSet)

    0 // return an integer exit code