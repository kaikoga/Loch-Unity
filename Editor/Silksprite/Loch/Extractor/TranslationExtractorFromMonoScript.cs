using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Silksprite.Loch.Extractor
{
    static class TranslationExtractorFromMonoScript
    {
        static readonly Regex Pattern = new Regex(@"(?<![0-9A-Za-z_])(?:Loc|Tr)\(""(.+?)""\)");

        public static IEnumerable<ExtractedLoc> ExtractFromMonoScript(string rootDirectory)
        {
            var extractedSourceCodes = AssetDatabase.FindAssets("t:MonoScript", new[] {
                    rootDirectory
                })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(assetPath =>
                {
                    var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
                    var sourceCode = asset.text!;

                    return (assetPath, sourceCode);
                });

            foreach (var r in extractedSourceCodes)
            {
                var (assetPath, sourceCode) = r;

                var lines = sourceCode.Split("\n").ToArray();
                for (var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    foreach (Match locMatch in Pattern.Matches(line))
                    {
                        yield return new ExtractedLoc(
                            locMatch.Groups[1].Value,
                            $"{Path.GetRelativePath(rootDirectory, assetPath)}:{i + 1}",
                            "");
                    }
                }
            }
        }
    }
}