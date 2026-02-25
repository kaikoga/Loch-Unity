using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Silksprite.Loch.Extractor
{
    static class TranslationExtractorFromUxml
    {
        public static IEnumerable<ExtractedLoc> ExtractFromUxml(string rootDirectory)
        {
            var extractedTrees = AssetDatabase.FindAssets("t:VisualTreeAsset", new[] {
                    rootDirectory
                })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(assetPath =>
                {
                    var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(assetPath);
                    var tree = JsonUtility.FromJson<ExtractedVisualTreeAsset>(JsonUtility.ToJson(asset));
                    tree.name = asset.name;
                    return (assetPath, tree);
                });

            foreach (var r in extractedTrees)
            {
                var (assetPath, tree) = r;
                foreach (var element in tree.m_VisualElementAssets)
                {
                    var hasValue = false;
                    string? nameValue = null;
                    string? locValue = null;
                    string? defaultValue = null;
                    var properties = element.m_Properties;
                    for (var i = 0; i < properties.Length; i += 2)
                    {
                        switch (properties[i])
                        {
                            case "text":
                            case "label":
                                defaultValue = properties[i + 1];
                                hasValue = true;
                                break;
                            case "name":
                                nameValue = $"{tree.name}::{properties[i + 1]}";
                                break;
                            case "loc":
                                locValue = properties[i + 1];
                                break;
                        }
                    }

                    if (hasValue && (locValue ?? nameValue) is { } loc)
                    {
                        yield return new ExtractedLoc(
                            loc,
                            Path.GetRelativePath(rootDirectory, assetPath),
                            defaultValue);
                    }
                }
            }
        }

        [Serializable]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        class ExtractedVisualTreeAsset
        {
            public string name = null!;
            [SerializeField]
            public ExtractedVisualElementAsset[] m_VisualElementAssets = null!;
        }
        [Serializable]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        class ExtractedVisualElementAsset
        {
            [SerializeField]
            public string[] m_Properties = null!;
        }
    }
}