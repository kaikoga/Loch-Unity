using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.Core;
using UnityEngine;

namespace Silksprite.Loch
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "Unity.BurstAccessingManagedMethod")]
    [SuppressMessage("ReSharper", "Unity.BurstFunctionSignatureContainsManagedTypes")]
    [SuppressMessage("ReSharper", "Unity.BurstLoadingManagedType")]
    public readonly struct LocalizedContent : IEquatable<LocalizedContent>
    {
        readonly string _key;
        internal readonly Assembly Assembly;
        internal readonly ReadOnlySubstitution Substitution;

        public LocalizedContent(string key, Assembly assembly)
        {
            _key = key;
            Assembly = assembly;
            Substitution = ReadOnlySubstitution.Empty;
        }

        public LocalizedContent Format(Substitution substitution) => new LocalizedContent(_key, Assembly, substitution);

        LocalizedContent(string key, Assembly assembly, Substitution substitution)
        {
            _key = key;
            Assembly = assembly;
            Substitution = substitution.ToReadOnly();
        }

        public string Tr => MaybeFormat(LochRepository.Instance.Tr(_key, Assembly));
        public string? TryTr => TryMaybeFormat(LochRepository.Instance.TryTr(_key, Assembly));
        public string LongTr => MaybeFormat(LochRepository.Instance.LongTr(_key, Assembly));
        public string? TryLongTr => TryMaybeFormat(LochRepository.Instance.TryLongTr(_key, Assembly));
        public GUIContent GUIContent => Substitution.HasValue ? new GUIContent(Tr) : LochRepository.Instance.GUIContent(_key, Assembly);

        string MaybeFormat(string value) => Substitution.HasValue ? Substitution.Format(value) : value;
        string? TryMaybeFormat(string? value) => value != null ? MaybeFormat(value) : null;
        
        public string TrFormat(Substitution substitution) => substitution.Format(Tr);
        public string LongTrFormat(Substitution substitution) => substitution.Format(LongTr);
        public GUIContent GUIContentFormat(Substitution substitution) => new GUIContent(substitution.Format(Tr));

        public bool Equals(LocalizedContent other)
        {
            return _key == other._key && Assembly.Equals(other.Assembly) && Equals(Substitution, other.Substitution);
        }
        public override bool Equals(object? obj)
        {
            return obj is LocalizedContent other && Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_key, Assembly, Substitution);
        }
        public static bool operator ==(LocalizedContent left, LocalizedContent right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(LocalizedContent left, LocalizedContent right)
        {
            return !left.Equals(right);
        }
    }
}