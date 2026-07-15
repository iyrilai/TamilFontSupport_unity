using UnityEditor;
using UnityEngine;

namespace Iyrilai.TamilFontFixer.Editor
{
    public class TamilFontFixerSettingsWindow : EditorWindow
    {
        TamilFontFixerSettings settings;
        SerializedObject serializedSettings;

        SerializedProperty fontAssetProp;
        SerializedProperty defaultEncodingProp;
        SerializedProperty dynamicallyLoadAddTamilTextProp;
        SerializedProperty dynamicallyLoadOnEditorProp;

        readonly GUIContent fontAssetContent = new(
            "Font Asset",
            "Default font used by TamilText."
        );

        readonly GUIContent defaultEncodingContent = new(
            "Default Encoding",
            "Encoding of font. Choose the correct one else text won't render properly."
        );

        readonly GUIContent dynamicallyLoadAddTamilTextContent = new(
            "Dynamically Load Add Tamil Text Fixer",
            "Auto add TamilTextFixer to TMP_Text and fix the font itself.\n\n⚠️ WARNING: This is an experimental feature!"
        );

        readonly GUIContent dynamicallyLoadOnEditorContent = new(
            "Dynamically Load On Editor",
            "Add TamilTextFixer on Editor as well.\n\n⚠️ WARNING: This is an experimental feature!"
        );

        [MenuItem("Tools/Tamil Font Fixer Settings")]
        static void Open()
        {
            var window = GetWindow<TamilFontFixerSettingsWindow>("Tamil Font Fixer Settings");
            window.minSize = new Vector2(180, 180);
        }

        void OnEnable()
        {
            LoadSettings();
        }

        void LoadSettings()
        {
            settings = TamilFontFixerSettings.Get();
            if (settings != null)
            {
                serializedSettings = new SerializedObject(settings);

                fontAssetProp = serializedSettings.FindProperty("fontAsset");
                defaultEncodingProp = serializedSettings.FindProperty("defaultEncoding");
                dynamicallyLoadAddTamilTextProp = serializedSettings.FindProperty("dynamicallyLoadAddTamilText");
                dynamicallyLoadOnEditorProp = serializedSettings.FindProperty("dynamicallyLoadOnEditor");
            }
        }

        void OnGUI()
        {
            if (settings == null || serializedSettings == null)
            {
                LoadSettings();
                if (settings == null) return;
            }

            serializedSettings.Update();

            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = Mathf.Max(220f, position.width * 0.65f);

            EditorGUILayout.Space(5);

            // --- Main Settings ---
            EditorGUILayout.LabelField("General Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(fontAssetProp, fontAssetContent);
            EditorGUILayout.PropertyField(defaultEncodingProp, defaultEncodingContent);

            EditorGUILayout.Space(10);

            // --- Experimental Section ---
            EditorGUILayout.LabelField("Experimental", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(dynamicallyLoadAddTamilTextProp, dynamicallyLoadAddTamilTextContent);

            if (dynamicallyLoadAddTamilTextProp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(dynamicallyLoadOnEditorProp, dynamicallyLoadOnEditorContent);
                EditorGUI.indentLevel--;
            }

            EditorGUIUtility.labelWidth = originalLabelWidth;
            serializedSettings.ApplyModifiedProperties();
        }
    }
}