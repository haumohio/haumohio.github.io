open System
open System.IO

let outputDir = "docs/"
let resourceDir = "wwwroot/"
let templateDir = "templates/"
let partialDir = templateDir + "partials"

let partials = 
  Directory.CreateDirectory(partialDir).GetFiles()
  |> Array.map ( fun f -> 
    (f.Name |> Path.GetFileNameWithoutExtension, f.FullName |> File.ReadAllText)
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
  Directory.CreateDirectory(templateDir) |> ignore
  Directory.CreateDirectory(outputDir) |> ignore
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
  Directory.CreateDirectory(resourceDir) |> ignore
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
  if Directory.Exists(outputDir) then Directory.Delete(outputDir, true) |> ignore
  processSite() 
  0 // return an integer exit code