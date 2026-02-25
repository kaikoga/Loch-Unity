using JetBrains.Annotations;
using UnityEditor;
using UnityEngine.Assertions;

namespace Silksprite.Loch.Extensions
{
    [PublicAPI]
    public static class LocalizedPropertyExtension
    {
        public static LocalizedProperty Lop(this SerializedObject self, string propertyPath, LocalizedContent loc)
        {
            var property = self.FindProperty(propertyPath);
            Assert.IsNotNull(property);
            return new LocalizedProperty(property, loc);
        }

        public static LocalizedProperty Lop(this SerializedProperty self, string propertyPath, LocalizedContent loc)
        {
            var property = self.FindPropertyRelative(propertyPath);
            Assert.IsNotNull(property);
            return new LocalizedProperty(property, loc);
        }

        public static LocalizedProperty Lop(this LocalizedProperty self, string propertyPath, LocalizedContent loc)
        {
            var property = self.Property.FindPropertyRelative(propertyPath);
            Assert.IsNotNull(property);
            return new LocalizedProperty(property, loc);
        }

        public static LocalizedProperty GetArrayElementAtIndex(this LocalizedProperty self, int index, LocalizedContent loc)
        {
            var property = self.Property.GetArrayElementAtIndex(index);
            Assert.IsNotNull(property);
            return new LocalizedProperty(property, loc);
        }
    }
}
