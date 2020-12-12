// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

let data = "../../../data/day_12.txt"

let readLines filePath = 
    System.IO.File.ReadAllLines(filePath) 
    |> Seq.map(fun x -> (x.[0], Int32.Parse x.[1..]))
    |> Seq.toList

let rec move (lines: Tuple<char, int> list) x y angle = 
    if lines.Length = 0 then
        x, y, angle
    else
        let cmd, arg = lines.[0]
        let newLines = lines.[1..]

        match cmd with
        | 'L' -> move newLines x y ((angle - arg) % 360)
        | 'R' -> move newLines x y ((angle + arg) % 360)
        | 'N' -> move newLines x (y + arg) angle
        | 'S' -> move newLines x (y - arg) angle
        | 'W' -> move newLines (x - arg) y angle
        | 'E' -> move newLines (x + arg) y angle
        | _ ->
            let rad = Math.PI * float(angle) / 180.0

            let x = x + (float(arg) * Math.Sin(rad) |> Math.Round |> int)
            let y = y + (float(arg) * Math.Cos(rad) |> Math.Round |> int)

            move newLines x y angle

let rec move2 (lines: Tuple<char, int> list) (shipX: int) (shipY: int) wpX wpY =
    if lines.Length = 0 then
        shipX, shipY
    else
        let cmd, arg = lines.[0]
        let newLines = lines.[1..]

        let wpX, wpY = 
            match cmd with
            | 'L' ->
                match arg with
                | 90 -> -wpY, wpX
                | 180 -> -wpX, -wpY
                | _ -> wpY, -wpX
            | 'R' ->
                match arg with
                | 90 -> wpY, -wpX
                | 180 -> -wpX, -wpY
                | _ -> -wpY, wpX
            | 'N' -> wpX, wpY + arg
            | 'S' -> wpX, wpY - arg
            | 'W' -> wpX - arg, wpY
            | 'E' -> wpX + arg, wpY
            | _ -> wpX, wpY

        let shipX, shipY =
            if cmd = 'F' then
                shipX + wpX * arg, shipY + wpY * arg
            else
                shipX, shipY
        
        move2 newLines shipX shipY wpX wpY

let q1 lines =
    let x, y, angle = move lines 0 0 90
    printfn "%d %d %d" x y angle
    (Math.Abs x) + (Math.Abs y)

let q2 lines =
    let shipX, shipY = move2 lines 0 0 10 1
    printfn "%d %d" shipX shipY
    (Math.Abs shipX) + (Math.Abs shipY)

[<EntryPoint>]
let main argv =
    let lines = readLines data
    printfn "%d" (q1 lines)
    printfn "%d" (q2 lines)
    0 // return an integer exit code