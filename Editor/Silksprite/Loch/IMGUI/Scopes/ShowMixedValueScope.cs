using System;
using JetBrains.Annotations;
using UnityEditor;

namespace Silksprite.Loch.IMGUI.Scopes
{
    [PublicAPI]
    public class ShowMixedValueScope : IDisposable
    {
        readonly bool _showMixedValue;

        public ShowMixedValueScope(LocalizedProperty lop) : this(lop.Property.hasMultipleDifferentValues) { }

        public ShowMixedValueScope(SerializedProperty property) : this(property.hasMultipleDifferentValues) { }

        public ShowMixedValueScope(bool showMixedValue)
        {
            _showMixedValue = EditorGUI.showMixedValue;
            EditorGUI.showMixedValue = showMixedValue;
        }

        public void Dispose()
        {
            EditorGUI.showMixedValue = _showMixedValue;
        }
    }
}
