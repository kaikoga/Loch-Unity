using System;

namespace Silksprite.Loch.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Enum)]
    public class LEnumAttribute : Attribute
    {
        public readonly Type? EnumType;

        public LEnumAttribute(Type? enumType = null) => EnumType = enumType;
    }
}
