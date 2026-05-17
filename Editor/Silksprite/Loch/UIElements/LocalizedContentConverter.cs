#if UNITY_2023_2_OR_NEWER

using System.Reflection;
using JetBrains.Annotations;
using UnityEditor.UIElements;

namespace Silksprite.Loch.UIElements.Base
{
    [UsedImplicitly]
    public class LocalizedContentConverter : UxmlAttributeConverter<LocalizedContent>
    {
        public override LocalizedContent FromString(string value)
        {
            // FIXME
            return new LocalizedContent(value, Assembly.GetCallingAssembly());
        }

        public override string ToString(LocalizedContent value)
        {
            // FIXME
            return value.Tr;
        }
    }

    [UsedImplicitly]
    public class NullableLocalizedContentConverter : UxmlAttributeConverter<LocalizedContent?>
    {
        public override LocalizedContent? FromString(string value)
        {
            // FIXME
            return new LocalizedContent(value, Assembly.GetCallingAssembly());
        }

        public override string ToString(LocalizedContent? value)
        {
            return value is { } loc ? loc.Tr : "";
        }
    }
}

#endif