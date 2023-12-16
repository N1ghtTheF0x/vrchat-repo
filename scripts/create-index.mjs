import { readFileSync, readdirSync, writeFileSync } from "node:fs"
import { dirname, resolve } from "node:path"
import { fileURLToPath } from "node:url"

const __dirname = dirname(fileURLToPath(import.meta.url))
const PackagesPath = resolve(__dirname,"..","Packages")
const indexPath = resolve(__dirname,"..","index.json")

const packages = readdirSync(PackagesPath,{withFileTypes: true}).filter((folder) => folder.name.startsWith("ntf.vrchat") && folder.isDirectory())
                
const packagesPath = packages
                    .map((folder) => resolve(folder.path,folder.name,"package.json"))

const index = JSON.parse(readFileSync(indexPath,"utf-8"))

for(let i = 0;i < packages.length;i++)
{
    const pak = packages[i].name
    const path = packagesPath[i]
    const obj = JSON.parse(readFileSync(path,"utf-8"))
    if(!index.packages[pak])
        index.packages[pak] = {
            versions: {}
        }

    index.packages[pak].versions[obj.version] = obj
}

writeFileSync(indexPath,JSON.stringify(index,null,4),"utf-8")