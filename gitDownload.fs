namespace gitDownload.fs

module Program =
    open System
    open Types
    open gitDownloadLib
    open System.Net
    open FSharp.Data

    [<EntryPoint>]
    let main(args) =    
       
        let mutable repo = Repo.Default

        args
        |> String.concat " "
        |> getMatches
        |> fun n-> 
            repo <- n 
        
        // printfn "repo.dir: %O"  repo

        repo
        |> getContentList
        |> processContentList
        |> fun n-> filterContentList n repo.dir
        |> downloadContentList
        |> ignore

        0



