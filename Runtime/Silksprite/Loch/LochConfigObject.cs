using System.Linq;
using Silksprite.Loch.Core;
using UnityEngine;

namespace Silksprite.Loch
{
    [CreateAssetMenu(fileName = "LochConfig", menuName = "Silksprite/Loch Config Object", order = 0)]
    public class LochConfigObject : ScriptableObject
    {
        [SerializeField] internal string key = "Loch";
        [SerializeField] internal string[] assemblies = { };
        [SerializeField] internal string[] assemblyPrefixes = { };
        [SerializeField] internal LocalizationAsset?[] po = { };

        internal LochDomain LochDomain => new LochDomain(
            key,
            po.Where(p => p).Select(p => new LocalizationProvider(p!)),
            assemblies.Where(assembly => !string.IsNullOrWhiteSpace(assembly)),
            assemblyPrefixes.Where(assemblyPrefix => !string.IsNullOrWhiteSpace(assemblyPrefix)));

        void OnValidate()
        {
            LochDomainSourceRegistry.Instance.ReloadSources();
        }
    }
}
