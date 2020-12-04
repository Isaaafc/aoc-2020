// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.Text.RegularExpressions

let data = "../../../data/day_4.txt"

let readLines filePath = System.IO.File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine) |> Array.map(fun x -> " " + x.Replace(Environment.NewLine, " ").Trim() + " ")

let cnt (lines: string []) (fields: string []) = 
    seq {
        for line in lines do
            let fCnt = 
                seq {
                    for f in fields do
                        let m = Regex.Match(line, f)
                        if m.Success then 1 else 0
                } |> Seq.reduce(( + ))

            if fCnt = fields.Length then
                1
            else
                0
    } |> Seq.reduce(( + ))

let q1 lines = 
    let fields = [|
        " byr:";
        " iyr:";
        " eyr:";
        " hgt:";
        " hcl:";
        " ecl:";
        " pid:"
    |]

    cnt lines fields

let q2 lines = 
    let fields = [|
        " byr:((19[2-9][0-9])|(200[0-2])) ";
        " iyr:((201[0-9])|(2020)) ";
        " eyr:((202[0-9])|(2030)) ";
        " hgt:((1[5-8][0-9]cm)|(19[0-3]cm)|(59in)|(6[0-9]in)|(7[0-6]in)) ";
        " hcl:(#[0-9a-f]{6}) ";
        " ecl:((amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)) ";
        " pid:([0-9]{9}) "
    |]

    cnt lines fields

[<EntryPoint>]
let main argv =
    let lines = readLines(data)
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code