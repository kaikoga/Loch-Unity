using System;
using System.Linq;
using System.Reflection;

namespace Silksprite.Loch.Core.Reflection
{
    class LEnumData
    {
        public readonly Assembly Assembly;
        public readonly Enum[] Values;
        public readonly string[] DisplayKeys;

        LEnumData(Assembly assembly, Enum[] values, string[] displayKeys)
        {
            Assembly = assembly;
            Values = values;
            DisplayKeys = displayKeys;
        }

        public static LEnumData From(Type type)
        {
            var values = type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => field.GetValue(null))
                .OfType<Enum>().ToArray();
            // var values = Enum.GetValues(type).OfType<Enum>().ToArray();
            var displayKeys = values.Select(Key).ToArray();
            return new LEnumData(type.Assembly, values, displayKeys);
        }

        public static string Key(Enum enumValue)
        {
            return $"{enumValue.GetType().Name}::{enumValue}";
        }
    }
}

