using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.Core;
using Unity.Burst;
using UnityEngine;

namespace Silksprite.Loch
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "Unity.BurstFunctionSignatureContainsManagedTypes")]
    [SuppressMessage("ReSharper", "Unity.BurstLoadingManagedType")]
    public readonly struct LocalizedContent : IEquatable<LocalizedContent>
    {
        readonly string _key;
        internal readonly Assembly Assembly;

        public LocalizedContent(string key, Assembly assembly)
        {
            _key = key;
            Assembly = assembly;
        }

        public string Tr => LochRepository.Instance.Tr(_key, Assembly);
        public string? TryTr => LochRepository.Instance.TryTr(_key, Assembly);
        public string LongTr => LochRepository.Instance.LongTr(_key, Assembly);
        public string? TryLongTr => LochRepository.Instance.TryLongTr(_key, Assembly);
        public GUIContent GUIContent => LochRepository.Instance.GUIContent(_key, Assembly);

        public string TrFormat(Substitution substitution) => substitution.Format(LochRepository.Instance.Tr(_key, Assembly));
        public string LongTrFormat(Substitution substitution) => substitution.Format(LochRepository.Instance.LongTr(_key, Assembly));
        public GUIContent GUIContentFormat(Substitution substitution) => new GUIContent(substitution.Format(LochRepository.Instance.Tr(_key, Assembly)));


        public bool Equals(LocalizedContent other)
        {
            return _key == other._key && Assembly.Equals(other.Assembly);
        }
        public override bool Equals(object? obj)
        {
            return obj is LocalizedContent other && Equals(other);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_key, Assembly);
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