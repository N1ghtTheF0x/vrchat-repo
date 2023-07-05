const IndexFile = {
    name: "N1ghtTheF0x's VRChat Packages",
    id: "ntf.vrchat.packages",
    url: "https://n1ghtthef0x.github.io/vrchat-repo/index.json",
    author: "N1ghtTheF0x",
    packages: {}
}

/**
 * 
 * @param {import("./Repository.cjs").VRChatRepository} index 
 * @param  {ReadonlyArray<import("./../packages/Package.cjs").VRChatPackageImport>} paks 
 */
function AddRepo(index,...paks)
{
    index.packages = {}
    for(const pak of paks)
    {
        index.packages[pak.id] = {
            versions: pak.versions
        }
    }
    return index
}

module.exports = {
    IndexFile,
    AddRepo
}