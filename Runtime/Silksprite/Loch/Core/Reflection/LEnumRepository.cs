using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Silksprite.Loch.Attributes;

namespace Silksprite.Loch.Core.Reflection
{
    class LEnumRepository
    {
        public static readonly LEnumRepository Instance = new LEnumRepository();

        readonly HashSet<Type> _lEnums;
        readonly Dictionary<Type, LEnumData> _lEnumData = new Dictionary<Type, LEnumData>();
        
        LEnumRepository()
        {
            _lEnums = CollectLEnumTypes().ToHashSet();
        }

        public bool TryGetData(Type type, [MaybeNullWhen(false)] out LEnumData data)
        {
            if (_lEnumData.TryGetValue(type, out data))
            {
                return true;
            }
            if (!_lEnums.Contains(type))
            {
                data = null;
                return false;
            }
            data = LEnumData.From(type);
            _lEnumData.Add(type, data);
            return true;
        }

        static IEnumerable<Type> CollectLEnumTypes() => CollectLEnumTypes(AppDomain.CurrentDomain.GetAssemblies());

        public static IEnumerable<Type> CollectLEnumTypes(LochConfigObject lochConfig)
        {
            return CollectLEnumTypes(AppDomain.CurrentDomain.GetAssemblies()
                .Where(assembly =>
                {
                    var name = assembly.GetName().Name;
                    return lochConfig.assemblies.Contains(name) || lochConfig.assemblyPrefixes.Any(prefix => name.StartsWith(prefix));
                })).Distinct();
        }

        static IEnumerable<Type> CollectLEnumTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(CollectLEnumTypes);
        }

        static IEnumerable<Type> CollectLEnumTypes(Assembly assembly)
        {
            foreach (var lEnum in assembly.GetCustomAttributes<LEnumAttribute>())
            {
                if (lEnum.EnumType != null)
                {
                    yield return lEnum.EnumType;
                }
            }
            foreach (var type in assembly.GetTypes().Where(type => type.IsEnum))
            {
                foreach (var lEnum in type.GetCustomAttributes<LEnumAttribute>())
                {
                    yield return lEnum.EnumType ?? type;
                }
            }
        }
    }
}
