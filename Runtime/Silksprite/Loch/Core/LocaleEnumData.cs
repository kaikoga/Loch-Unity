using System;
using System.Linq;
using System.Reflection;
using Silksprite.Loch.Core.Reflection;
using UnityEngine;

namespace Silksprite.Loch.Core
{
    class LocaleEnumData
    {
        public readonly Enum[] Values;
        public readonly LocalizedContent[] Locs;
        public readonly GUIContent[] DisplayGUIContents;

        LocaleEnumData(Enum[] values, LocalizedContent[] locs, GUIContent[] displayGUIContents)
        {
            Values = values;
            Locs = locs;
            DisplayGUIContents = displayGUIContents;
        }

        public static LocaleEnumData From(LEnumData data, Assembly? assembly)
        {
            var values = data.Values;
            var locs = data.DisplayKeys
                .Select(key => new LocalizedContent(key, assembly ?? data.Assembly))
                .ToArray();
            var displayGUIContents = locs
                .Select(loc => loc.GUIContent)
                .ToArray();
            return new LocaleEnumData(values, locs, displayGUIContents);
        }
    }
}

