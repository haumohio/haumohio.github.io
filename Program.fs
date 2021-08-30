open System
open System.IO

let outputDir = "docs/"
let resourceDir = "wwwroot/"
let templateDir = "templates/"

let partials = 
  Directory.GetFiles(templateDir + "partials")
  |> Array.map ( fun f -> 
    (f |> Path.GetFileNameWithoutExtension, f |> File.ReadAllText)
  )

let insertPartials (content: string) =
  partials
  |> Array.fold (
    fun (c:string) p -> 
      let lookup = "{{ " + (fst p) + " }}"
      let replacement = snd p
      c.Replace( lookup, replacement)
    ) content
  

let processSite() =
  let files = Directory.GetFiles(templateDir)
  files 
  |> Array.map (fun f -> 
    let filename = Path.GetFileName f
    let content = 
      File.ReadAllText f
      |> insertPartials
    File.WriteAllText(outputDir + filename, content)
  )
  |> ignore

  let resources = Directory.GetFiles(resourceDir, "*", SearchOption.AllDirectories)
  resources 
  |> Array.map (fun f -> 
    let filename = outputDir + f.Substring(resourceDir.Length)
    Directory.CreateDirectory(Path.GetDirectoryName filename ) |> ignore
    File.Copy(f, filename, true)
  )
  |> ignore


[<EntryPoint>]
let main argv =
    Directory.Delete(outputDir, true) |> ignore
    Directory.CreateDirectory(outputDir) |> ignore
    processSite() 
    0 // return an integer exit code