using UnityEditor;

namespace Silksprite.Loch.Core.Settings
{
    static class LocaleSettingRepositoryImpl
    {
        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            Rebind();
        }

        public static void Rebind()
        {
#if LOCH_NDMF_SUPPORT
            if (NdmfSyncSettingRepository.Instance.IsNdmfSyncEnabled)
            {
                LocaleSettingRepository.Instance = new NdmfLocaleSettingRepository();
            }
            else
#endif
            {
                LocaleSettingRepository.Instance = new EditorPrefsLocaleSettingRepository();
            }
        }
    }
}