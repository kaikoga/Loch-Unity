using System.Linq;
using UnityEditor;

namespace Silksprite.Loch.Core.Settings
{
    class AssetDatabaseLochDomainSource : AssetPostprocessor, ILochDomainSource
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            LochDomainSourceRegistry.Instance.Add(new AssetDatabaseLochDomainSource());
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            LochDomainSourceRegistry.Instance.ReloadSources();
        }
        
        void ILochDomainSource.Load(LochRepository repository)
        {
            var domains = AssetDatabase.FindAssets("t:LochConfigObject")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<LochConfigObject>)
                .Select(config => config.LochDomain);
            foreach (var domain in domains)
            {
                repository.Add(domain);
            }
        }
    }
}
