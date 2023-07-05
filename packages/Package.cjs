if(typeof window != "undefined") throw new Error("Only for NodeJS!")

const fs = require("node:fs")
const path = require("node:path")

function readPackage(p)
{
    return JSON.parse(fs.readFileSync(path.resolve(__dirname,p),"utf-8"))
}

module.exports = {
    readPackage
}