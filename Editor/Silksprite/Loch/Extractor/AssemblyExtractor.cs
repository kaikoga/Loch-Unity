using System;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Silksprite.Loch.Extractor
{
    static class AssemblyExtractor
    {
        public static void ExtractAssemblies(LochConfigObject target)
        {
            var path = AssetDatabase.GetAssetPath(target);
            var localeDirectory = Path.GetDirectoryName(path);
            var rootDirectory = Path.GetDirectoryName(localeDirectory)!;


            var extractedAssemblies = AsssemblyExtractorFromAssemblyDefinition.ExtractFromAssemblyDefinition(rootDirectory)
                .Distinct()
                .OrderBy(assembly => assembly);

            target.assemblies = extractedAssemblies.ToArray();
            target.assemblyPrefixes = Array.Empty<string>();
        }
    }
}
