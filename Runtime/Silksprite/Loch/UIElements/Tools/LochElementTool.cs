using System;
using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.UIElements.Base;
using UnityEngine.UIElements;

namespace Silksprite.Loch.UIElements.Tools
{
    [PublicAPI]
    public static class LochElementTool
    {
        public static T LocalizeWith<T>(this T container, Assembly assembly, string prefix) where T : VisualElement
        {
            foreach (var child in container.Children())
            {
                if (child is ILocElement locElement)
                {
                    locElement.loc = locElement.loc is { } loc
                        ? loc.WithAssembly(assembly)
                        : new LocalizedContent($"{prefix}::{child.name}", assembly);
                }
                else
                {
                    child.LocalizeWith(assembly, prefix);
                }
            }
            return container;
        }

        public static T LocalizeWith<T>(this T container, Type type) where T : VisualElement => container.LocalizeWith(type.Assembly, type.Name);

        public static void Localize<T>(this VisualElement container) => container.LocalizeWith(typeof(T));
    }
}
