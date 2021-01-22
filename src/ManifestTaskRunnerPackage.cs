using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace ManifestTaskRunner
{
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("74fcd30a-f061-4579-bb05-e39cb20431e9")]
    public sealed class ManifestTaskRunnerPackage : AsyncPackage
    {
    }
}
