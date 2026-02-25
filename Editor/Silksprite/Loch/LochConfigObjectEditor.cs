using Silksprite.Loch.Extensions;
using Silksprite.Loch.Extractor;
using Silksprite.Loch.IMGUI;
using UnityEditor;
using static Silksprite.Loch.Tools.LochTool;

namespace Silksprite.Loch
{
    [CustomEditor(typeof(LochConfigObject))]
    class LochConfigObjectEditor : Editor
    {
        LocalizedProperty _key = null!;
        LocalizedProperty _assemblies = null!;
        LocalizedProperty _assemblyPrefixes = null!;
        LocalizedProperty _po = null!;

        LocalizedProperty Lop(string propertyPath, LocalizedContent loc) => serializedObject.Lop(propertyPath, loc);

        void OnEnable()
        {
            _key = Lop(nameof(LochConfigObject.key), Loc("LochConfigObject::key"));
            _assemblies = Lop(nameof(LochConfigObject.assemblies), Loc("LochConfigObject::assemblies"));
            _assemblyPrefixes = Lop(nameof(LochConfigObject.assemblyPrefixes), Loc("LochConfigObject::assemblyPrefixes"));
            _po = Lop(nameof(LochConfigObject.po), Loc("LochConfigObject::po"));
        }

        public override void OnInspectorGUI()
        {
            LEditorGUILayout.LocaleSelector();
            LEditorGUILayout.Prop(_key);
            LEditorGUILayout.Prop(_assemblies);
            LEditorGUILayout.Prop(_assemblyPrefixes);
            LEditorGUILayout.Prop(_po);
            serializedObject.ApplyModifiedProperties();

            LGUILayout.Heading(Loc("LochConfigObject::utilitiesForLochPackages"));

            if (LGUILayout.Button(Loc("LochConfigObject::extractAssemblies")))
            {
                Undo.RecordObject(target, Loc("LochConfigObject::extractAssemblies").Tr);
                AssemblyExtractor.ExtractAssemblies((LochConfigObject)target);
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssetIfDirty(target);
                serializedObject.Update();
            }

            if (LGUILayout.Button(Loc("LochConfigObject::generatePotFile")))
            {
                TranslationExtractor.ExtractTranslations((LochConfigObject)target);
            }
        }
    }
}
