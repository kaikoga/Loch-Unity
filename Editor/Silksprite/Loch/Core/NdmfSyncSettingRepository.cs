using Silksprite.Loch.Core.Settings;
using UnityEditor;

namespace Silksprite.Loch.Core
{
    public class NdmfSyncSettingRepository
    {
        public static readonly NdmfSyncSettingRepository Instance = new NdmfSyncSettingRepository();

        const string EditorPrefKey = "net.kaikoga.Loch.ndmfSync";

        public bool IsNdmfSyncEnabled
        {
            get => EditorPrefs.GetBool(EditorPrefKey, true);
            set
            {
                if (IsNdmfSyncEnabled == value)
                {
                    return;
                }
                EditorPrefs.SetBool(EditorPrefKey, value);
                LocaleSettingRepositoryImpl.Rebind();
            }
        }
    }
}
