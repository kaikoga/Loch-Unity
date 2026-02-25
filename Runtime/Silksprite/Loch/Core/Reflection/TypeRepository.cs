using System;
using System.Collections.Generic;
using System.Linq;

namespace Silksprite.Loch.Core.Reflection
{
    class TypeRepository
    {
        public static readonly TypeRepository Instance = new TypeRepository();

        readonly Dictionary<string, Type?> _typeCache = new Dictionary<string, Type?>();

        public Type? GetType(string name)
        {
            if (_typeCache.TryGetValue(name, out var type))
            {
                return type;
            }
            type = FindType(name);
            _typeCache.Add(name, type);
            return type;
        }

        Type? FindType(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.Name == name);
        }
    }
}
