// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

let data = "../../../data/day_5.txt"

let readLines filePath = System.IO.File.ReadAllLines(filePath) |> Seq.toList

let rec split (line: string) l r lc rc =
    let c = line.[0]
    let half = 1 + (r - l) / 2

    let _l = if c = lc then l + half else l
    let _r = if c = rc then r - half else r

    if _r > _l then
        split (line.Substring 1) _l _r lc rc
    else
        _l

let seat (line: string) = 
    let row = split (line.[0..6]) 0 127 'B' 'F'
    let col = split (line.[7..9]) 0 7 'R' 'L'

    row * 8 + col

let q1 lines =
    lines |> List.map(seat) |> List.max

let q2 lines =
    let seats = lines |> List.map(seat) |> List.sort

    seq {
        for i in [seats.[0]..seats.[seats.Length - 1]] do
            if (not (List.contains i seats)) && (List.contains (i + 1) seats) && (List.contains (i - 1) seats) then
                i
            else
                0
    } |> Seq.reduce(( + ))

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code