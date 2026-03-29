using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Silksprite.Loch.Extractor
{
    static class TranslationExtractor
    {
        public static void ExtractTranslations(LochConfigObject target)
        {
            var path = AssetDatabase.GetAssetPath(target);
            var localeDirectory = Path.GetDirectoryName(path);
            var rootDirectory = Path.GetDirectoryName(localeDirectory)!;

            
            var potPath = Path.Join(localeDirectory, $"{target.name}.pot");
            var sb = new StringBuilder();
            sb.AppendLine(@"msgid """"");
            sb.AppendLine(@"msgstr """"");
            sb.AppendLine(@"""Project-Id-Version: \n""");
            sb.AppendLine($@"""POT-Creation-Date: {PotCreationDate()}\n""");
            sb.AppendLine(@"""PO-Revision-Date: \n""");
            sb.AppendLine(@"""Last-Translator: \n""");
            sb.AppendLine(@"""Language-Team: \n""");
            sb.AppendLine(@"""Language: en\n""");
            sb.AppendLine(@"""MIME-Version: 1.0\n""");
            sb.AppendLine(@"""Content-Type: text/plain; charset=UTF-8\n""");
            sb.AppendLine(@"""Content-Transfer-Encoding: 8bit\n""");
            sb.AppendLine(@"""X-Generator: Loch\n""");
            sb.AppendLine("");

            var extractedLocs = TranslationExtractorFromUxml.ExtractFromUxml(rootDirectory)
                .Concat(TranslationExtractorFromMonoScript.ExtractFromMonoScript(rootDirectory))
                .Concat(TranslationExtractorFromAssembly.ExtractFromAssembly(target))
                .GroupBy(loc => loc.MessageId)
                .OrderBy(g => g.Key);

            foreach (var g in extractedLocs)
            {
                foreach (var loc in g)
                {
                    sb.AppendLine($"#: {loc.SourcePosition}");
                }
                sb.AppendLine($@"msgid ""{g.Key}""");
                var defaultValue = g.Select(loc => loc.DefaultValue).FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
                sb.AppendLine($@"msgstr ""{defaultValue}""");
                sb.AppendLine("");
            }
            File.WriteAllText(potPath, sb.ToString());

        }
        static string PotCreationDate()
        {
            var now = DateTime.Now;
            var creationDate = now.ToString("yyyy-MM-dd hh:mm");
            var potCreationTimeZone = now.ToString("zzz").Replace(":", "");
            return creationDate + potCreationTimeZone;
        }
    }

    class ExtractedLoc
    {
        public readonly string MessageId;
        public readonly string SourcePosition;
        public readonly string DefaultValue;

        public ExtractedLoc(string messageId, string sourcePosition, string defaultValue)
        {
            MessageId = messageId;
            SourcePosition = sourcePosition;
            DefaultValue = defaultValue;
        }
    }
}
