using System;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Silksprite.Loch
{
    [PublicAPI]
    public class LocalizedProperty : IEquatable<LocalizedProperty>
    {
        public readonly SerializedProperty Property;
        public readonly LocalizedContent Loc;

        internal Assembly Assembly => Loc.Assembly;

        public GUIContent GUIContent => Loc.GUIContent;

        public LocalizedProperty(SerializedProperty property, LocalizedContent loc)
        {
            Property = property;
            Loc = loc;
        }

        public bool Equals(LocalizedProperty? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Property.Equals(other.Property) && Loc.Equals(other.Loc);
        }
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((LocalizedProperty)obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Property, Loc);
        }
        public static bool operator ==(LocalizedProperty? left, LocalizedProperty? right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(LocalizedProperty? left, LocalizedProperty? right)
        {
            return !Equals(left, right);
        }
    }
}