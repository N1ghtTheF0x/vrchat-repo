if(typeof window != "undefined") throw new Error("Only for NodeJS!")

const Packages = require("./packages/index.cjs")
const { IndexFile, AddRepo} = require("./repository/Index.cjs")

const result = AddRepo(IndexFile,...Packages)

const fs = require("node:fs")
const path = require("node:path")

fs.writeFileSync(path.resolve(__dirname,"index.json"), JSON.stringify(result,null,4))