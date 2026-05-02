using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Silksprite.Loch.Tools
{
    [PublicAPI]
    public readonly struct LochAssemblyScope : IDisposable
    {
        public LochAssemblyScope(Assembly assembly) => LochTool.CurrentAssembly = assembly;

        public void Dispose() => LochTool.CurrentAssembly = null;
    }
}