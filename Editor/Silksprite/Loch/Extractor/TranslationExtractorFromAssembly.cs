using System.Collections.Generic;
using Silksprite.Loch.Core.Reflection;

namespace Silksprite.Loch.Extractor
{
    static class TranslationExtractorFromAssembly
    {
        public static IEnumerable<ExtractedLoc> ExtractFromAssembly(LochConfigObject target)
        {
            var targetEnums = LEnumRepository.CollectLEnumTypes(target);

            foreach (var targetEnum in targetEnums)
            {
                var enumName = targetEnum.Name;
                foreach (var name in targetEnum.GetEnumNames())
                {
                    yield return new ExtractedLoc(
                        $"{enumName}::{name}",
                        "",
                        "");
                }
            }
        }
    }
}