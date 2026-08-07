using UnityEditor;
using UnityEngine;

namespace Iyrilai.TamilFontFixer.Editor
{
    [CustomEditor(typeof(TamilTextFixer))]
    public class TamilTextFixerEditor : UnityEditor.Editor
    {
        SerializedProperty overrideSettingProp;
        SerializedProperty fontAssetProp;
        SerializedProperty defaultEncodingProp;

        readonly GUIContent buttonContent = new("Add as component", "This will allow you to edit and save the Component.");

        void OnEnable()
        {
            overrideSettingProp = serializedObject.FindProperty("overrideSetting");
            fontAssetProp = serializedObject.FindProperty("fontAsset");
            defaultEncodingProp = serializedObject.FindProperty("defaultEncoding");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var tamilTextFixer = target as TamilTextFixer;

            if (tamilTextFixer != null && tamilTextFixer.hideFlags == HideFlags.HideAndDontSave && !EditorApplication.isPlaying)
            {
                GUI.enabled = true;

                EditorGUILayout.HelpBox("This component is currently hidden.", MessageType.Info);

                if (GUILayout.Button(buttonContent))
                {
                    tamilTextFixer.hideFlags = HideFlags.None;

                    EditorUtility.SetDirty(tamilTextFixer.gameObject);
                    AutoTamilTextFixerAssigner.RemoveFromAutoAssignedList(tamilTextFixer);
                }
            }

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