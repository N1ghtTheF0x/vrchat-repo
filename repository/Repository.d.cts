import { VRChatPackage } from "../packages/Package.d.cts"

declare interface VRChatRepository
{
    name: string
    id: string
    url: string
    author: string
    packages: Record<string,VRChatRepository.PackageEntry>
}

namespace VRChatRepository
{
    export interface PackageEntry
    {
        versions: Record<VRChatPackage.Version,VRChatPackage>
    }
}