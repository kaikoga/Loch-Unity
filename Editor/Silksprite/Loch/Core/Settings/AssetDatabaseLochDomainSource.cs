using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Silksprite.Loch.Core.Settings
{
    [DefaultExecutionOrder(int.MinValue)]
    class AssetDatabaseLochDomainSource : AssetPostprocessor, ILochDomainSource
    {
        [InitializeOnLoadMethod]
        [RuntimeInitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            LochDomainSourceRegistry.Instance.Add(new AssetDatabaseLochDomainSource());
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LochRepository.Instance.InvalidateDomainCache();
        }
        
        void ILochDomainSource.LoadDomains(ILochDomainLoader loader)
        {
            var domains = AssetDatabase.FindAssets("t:LochConfigObject")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<LochConfigObject>)
                .Select(config => config.LochDomain);
            foreach (var domain in domains)
            {
                loader.Load(domain);
            }
        }
    }
}
