using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Silksprite.Loch.Extractor
{
    static class AsssemblyExtractorFromAssemblyDefinition
    {
        public static IEnumerable<string> ExtractFromAssemblyDefinition(string rootDirectory)
        {
            return AssetDatabase.FindAssets("t:AssemblyDefinitionAsset", new[] {
                    rootDirectory
                })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(assetPath =>
                {
                    var asset = AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(assetPath);
                    var asmdef = JsonUtility.FromJson<ExtractedAssemblyDefinitionAsset>(asset.text);
                    return asmdef.name;
                });
        }

        [Serializable]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        class ExtractedAssemblyDefinitionAsset
        {
            public string name = null!;
        }
    }
}