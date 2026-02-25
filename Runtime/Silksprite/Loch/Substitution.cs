using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Silksprite.Loch
{
    [PublicAPI]
    public class Substitution
    {
        static readonly Regex Pattern = new Regex("{([^}]*)}");
        
        public static readonly Substitution Empty = new Substitution();

        readonly Dictionary<string, string> _dict;
        readonly Func<string, string> _mapping;

        public string this[string key]
        {
            set => _dict[key] = value;
        }

        public Substitution(Func<string, string>? mapping = null) : this(new Dictionary<string, string>(), mapping) { }

        Substitution(Dictionary<string, string> dict, Func<string, string>? mapping = null)
        {
            _dict = dict;
            _mapping = mapping ?? (key => $"{{{key}}}");
        }

        public string Format(string value)
        {
            var mapping = _mapping;
            return Pattern.Replace(value, match =>
            {
                var key = match.Groups[1].Value;
                return _dict.TryGetValue(key, out var v) ? v : mapping(key);
            });
        }
    }
}