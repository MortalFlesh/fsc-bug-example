module DomainResolverLibrary =
    open System.IO
    open FSharp.Compiler.CodeAnalysis
    open FSharp.Compiler.Text

    let private parseAndCheck (checker: FSharpChecker) (file, contents) =
        try
            let projOptions, errors =
                checker.GetProjectOptionsFromScript(file, SourceText.ofString contents)
                |> Async.RunSynchronously

            sprintf "ProjectOptions: %A" projOptions |> Ok

        with e ->
            sprintf "GetProjectOptionsFromScript.Error: %A" e |> Error

    let parse file =
        let checker = FSharpChecker.Create()

        (file, File.ReadAllText file)
        |> parseAndCheck checker

let separator () = String.replicate 20 "=" |> printfn "%s"

[<EntryPoint>]
let main argv =
    separator()
    printfn "FSCS.BugExample\n"

    let projectFile = "domain/domain.fsx"

    match projectFile |> DomainResolverLibrary.parse with
    | Ok message ->
        printfn "This works fine:\n%s" message
        separator()
        0

    | Error error ->
        eprintfn "This doesn't work as expected:\n%s" error
        separator()
        1
