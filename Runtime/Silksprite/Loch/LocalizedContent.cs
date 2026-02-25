using System;
using System.Reflection;
using JetBrains.Annotations;
using Silksprite.Loch.Core;
using UnityEngine;

namespace Silksprite.Loch
{
    [PublicAPI]
    public class LocalizedContent : IEquatable<LocalizedContent>
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

        public bool Equals(LocalizedContent? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return _key == other._key && Assembly.Equals(other.Assembly);
        }
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((LocalizedContent)obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(_key, Assembly);
        }
        public static bool operator ==(LocalizedContent? left, LocalizedContent? right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(LocalizedContent? left, LocalizedContent? right)
        {
            return !Equals(left, right);
        }
    }
}