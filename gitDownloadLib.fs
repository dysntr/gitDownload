namespace gitDownload.fs


module gitDownloadLib =
    open Types
    open System
    open System.Text.RegularExpressions
    open FSharp.Data
    open System.Net;
    open System.IO;

    // Takes the command line argument and returns the Repo Record
    let getMatches e =
        let urlParserRegex = Regex(@"github.com[/]([^/]+)[/]([^/]+)[/]tree[/]([^/]+)[/](.*)", RegexOptions.Compiled)
        let m: Match = urlParserRegex.Match(e)
        {user = m.Groups[1].Value; repo = m.Groups[2].Value; ref = m.Groups[3].Value; dir = m.Groups[4].Value}


    let fetchRepoInfo e =
        let url = "https://api.github.com/repos/" + e.user + "/" + e.repo
        async {
            return! Http.AsyncRequestString
                ( url,
                headers = [ "Content-Type", HttpContentTypes.Json; "User-Agent", "curl/7.81.0"; "accept", "*/*"]
                )
        } 
        |> Async.Catch 
        |> Async.RunSynchronously

    let getContentList e =
            let url = "https://api.github.com/repos/" + e.user + "/" + e.repo + "/git/trees/" + e.ref + "?recursive=1"
            async {
                return! Http.AsyncRequestString
                    ( url,
                    headers = [ "Content-Type", HttpContentTypes.Json; "User-Agent", "curl/7.81.0"; "accept", "*/*"]
                    )
            } 
            |> Async.Catch 
            |> Async.RunSynchronously
    
    let processContentList (result: Choice<string,exn>) =

            let urlParserRegex = Regex(@"([^/]+$)", RegexOptions.Compiled)

            match result with
            | Choice1Of2 text ->  
                let contentList = ContentList.Parse(text)
                printfn "Found Repo: %A" contentList.Url
                printfn ""
                contentList.Tree
                |> fun n -> 
                    [
                        for i in n do 
                            let m: Match = urlParserRegex.Match(i.Path)
                            {Path=i.Path; Type=i.Type; Url=i.Url; Name=m.Groups[1].Value}
                    ]
            | Choice2Of2 e -> 
                printfn "Repo Not Found: %A" e
                let x: DirContent list = []
                x

    let filterContentList (dList: DirContent list ) (dir: string) =
        dList
        //|> List.filter filterContentItem (dList) (dir)
        |> List.where (fun elem -> elem.Path.Contains(dir + "/") )
        |> List.where (fun elem -> elem.Type = "blob" )
        |> fun n -> 
            [
                for i in n do
                    printfn "Found File: %A" i.Name
                    i
            ]
    
    let downloadFile (url:string) (name:string) =
        printfn ""
        printfn "Downloading file: %A from %A" name url
        async {
            return! Http.AsyncRequestString
                ( url,
                headers = [ "Content-Type", HttpContentTypes.Json; "User-Agent", "curl/7.81.0"; "accept", "*/*"]
                )
        } 
        |> Async.Catch 
        |> Async.RunSynchronously
        
    let createFile (fileName) (content:string)=
        printfn "Download of %A Completed." fileName
        use streamWriter = new StreamWriter(fileName, false)
        content
        |> streamWriter.WriteLine
    
    let downloadContentList (dList: DirContent list) =
        dList
        |> fun n -> 
            [
                for i in n do 
                    downloadFile i.Url i.Name
                    |> fun n -> 
                            match n with
                            | Choice1Of2 text -> 
                                let file = FileSample.Parse(text)
                                file.Content.Replace("\n","")
                                |> System.Convert.FromBase64String
                                |> System.Text.Encoding.UTF8.GetString
                                |> fun x -> createFile i.Name x 
                            | Choice2Of2 e ->  ()                       
            ]      


