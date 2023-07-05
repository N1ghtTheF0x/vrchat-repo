declare interface VRChatPackage
{
    name: string
    displayName: string
    version: VRChatPackage.Version
    description: string
    gitDependencies: Record<string,string>
    author: VRChatPackage.Author
    legacyFolders: Record<string,string>
    legacyFiles: Record<string,string>
    license: string
    url: string
    samples: ReadonlyArray<VRChatPackage.Sample>
}

namespace VRChatPackage
{
    export type Version = `${number}.${number}.${number}`
    export interface Author
    {
        name: string
        email: string
        url: string
    }
    export interface Sample
    {
        displayName: string
        description: string
        path: string
    }
}

declare interface VRChatPackageImport
{
    id: string
    versions: Record<VRChatPackage.Version,VRChatPackage>
}

export function readPackage(path: string): VRChatPackageImport