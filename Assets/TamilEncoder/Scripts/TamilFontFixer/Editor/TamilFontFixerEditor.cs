using UnityEditor;

namespace Iyrilai.TamilFontFixer.Editor
{
    [CustomEditor(typeof(TamilTextFixer))]
    public class TamilTextFixerEditor : UnityEditor.Editor
    {
        private SerializedProperty overrideSettingProp;
        private SerializedProperty fontAssetProp;
        private SerializedProperty defaultEncodingProp;

        private void OnEnable()
        {
            overrideSettingProp = serializedObject.FindProperty("overrideSetting");
            fontAssetProp = serializedObject.FindProperty("fontAsset");
            defaultEncodingProp = serializedObject.FindProperty("defaultEncoding");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(overrideSettingProp);

            if (overrideSettingProp.boolValue)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(fontAssetProp);
                EditorGUILayout.PropertyField(defaultEncodingProp);

                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}