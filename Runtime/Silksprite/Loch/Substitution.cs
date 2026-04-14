using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Silksprite.Loch
{
    [PublicAPI]
    public struct Substitution
    {
        public static readonly Substitution Empty;

        Dictionary<string, string>? _dict;
        Dictionary<string, string> Dict => _dict ??= new Dictionary<string, string>();

        public bool HasValue => _dict != null;  

        public string this[string key]
        {
            set => Dict[key] = value;
        }

        public Substitution Merge(Substitution other)
        {
            if (other._dict is { } dict)
            {
                foreach (var key in dict)
                {
                    Dict.Add(key.Key, key.Value);
                }
            }
            return this;
        }

        public string Format(string value) => ToReadOnly().Format(value);

        internal ReadOnlySubstitution ToReadOnly() => new ReadOnlySubstitution(_dict);
    }

    readonly struct ReadOnlySubstitution
    {
        public static readonly ReadOnlySubstitution Empty;

        static readonly Regex Pattern = new Regex("{([^}]*)}");

        readonly Dictionary<string, string>? _dict;

        public bool HasValue => _dict != null;  

        public ReadOnlySubstitution(Dictionary<string, string>? dict) => _dict = dict;
        
        public string Format(string value)
        {
            return _dict is { } dict ? Pattern.Replace(value, match =>
                {
                    var key = match.Groups[1].Value;
                    return dict.TryGetValue(key, out var v) ? v : $"{{{key}}}";
                })
                : value;
        }
    }
}
