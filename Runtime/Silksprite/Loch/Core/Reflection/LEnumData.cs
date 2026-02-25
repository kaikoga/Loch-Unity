using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Silksprite.Loch.Core.Reflection
{
    class LEnumData
    {
        public readonly Assembly Assembly;
        readonly Dictionary<Enum, string> _keys;
        public readonly Enum[] Values;
        public readonly string[] DisplayKeys;

        LEnumData(Assembly assembly, Dictionary<Enum, string> keys, Enum[] values, string[] displayKeys)
        {
            Assembly = assembly;
            _keys = keys;
            Values = values;
            DisplayKeys = displayKeys;
        }

        public static LEnumData From(Type type)
        {
            var keys = Enum.GetValues(type).OfType<Enum>()
                .ToDictionary(enumValue => enumValue, Key);
            var values = keys.Keys.ToArray();
            var displayKeys = keys.Values.ToArray();
            return new LEnumData(type.Assembly, keys, values, displayKeys);
        }

        public static string Key(Enum enumValue)
        {
            return $"{enumValue.GetType().Name}::{enumValue}";
        }
    }
}

