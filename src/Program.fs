module DomainResolverLibrary =
    open System.IO
    open FSharp.Compiler.CodeAnalysis
    open FSharp.Compiler.Text

    let private parseAndCheck (checker: FSharpChecker) (file, contents) =
        try
            let projOptions, errors =
                checker.GetProjectOptionsFromScript(file, SourceText.ofString contents)
                |> Async.RunSynchronously

            printfn "ProjectOptions: %A" projOptions

            match errors with
            | [] -> ()
            | errors -> printfn "ProjectOptions.Errors: %A" errors

        with e ->
            eprintfn "GetProjectOptionsFromScript.Error: %A" e

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

    projectFile |> DomainResolverLibrary.parse

    separator()
    0
