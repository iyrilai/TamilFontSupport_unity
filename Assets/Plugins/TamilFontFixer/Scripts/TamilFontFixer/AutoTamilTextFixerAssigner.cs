using UnityEngine;
using TMPro;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Iyrilai.TamilFontFixer
{

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class AutoTamilTextFixerAssigner
    {
        static bool isInitialized;

        public static void RestartInitialize()
        {
            Initialize(true);
            EditorApplication.delayCall += ReloadTMPs;
        }

        static void ReloadTMPs()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
            {
                EditorApplication.delayCall += ReloadTMPs;
                return;
            }

            // Force update all TMP_Text components to trigger the TEXT_CHANGED_EVENT
            TMP_Text[] allTextComponents = Object.FindObjectsByType<TMP_Text>(FindObjectsSortMode.None);

            foreach (TMP_Text textComponent in allTextComponents)
            {
                if (textComponent == null)
                    continue;

                if (textComponent is TextMeshProUGUI UI)
                {
                    if (UI.canvasRenderer == null)
                        continue;
                }

                textComponent.ForceMeshUpdate();
            }
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void EditorInitializeOnLoadMethod()
        {
            Initialize(true);
        }

        static AutoTamilTextFixerAssigner()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                Cleanup();
            }
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RuntimeInitializeOnLoadMethod()
        {
            Initialize();
        }

        readonly static HashSet<TamilTextFixer> tamilTextFixers = new();

        static void Cleanup()
        {
            TamilFontFixerSettings settings = TamilFontFixerSettings.Get();

            if (settings != null && settings.DynamicallyLoadOnEditor)
            {
                return;
            }

            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
            isInitialized = false;

            EditorApplication.delayCall += CleanAssignedTamilTextFixers;
        }

        static void CleanAssignedTamilTextFixers()
        {
            foreach (var tamilTextFixer in tamilTextFixers)
            {
                if (tamilTextFixer != null)
                {
                    tamilTextFixer.Deactivated = true;
                    Object.DestroyImmediate(tamilTextFixer);
                }
            }

            tamilTextFixers.Clear();
            EditorApplication.delayCall -= CleanAssignedTamilTextFixers;
        }

        static void Initialize(bool isEditor = false)
        {
            TamilFontFixerSettings settings = TamilFontFixerSettings.Get();
            if (settings == null)
            {
                return;
            }

            if (isInitialized && isEditor && !settings.DynamicallyLoadOnEditor)
            {
                Cleanup();
            }

            if (isInitialized)
                return;


            if (isEditor && !settings.DynamicallyLoadOnEditor)
            {
                return;
            }

            if (!settings.DynamicallyLoadAddTamilText)
            {
                return;
            }

            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
            isInitialized = true;
        }

        static void OnTextChanged(Object obj)
        {
            if (obj is TMP_Text tmpText)
            {
                if (tmpText.TryGetComponent<TamilTextFixer>(out var existingTamilTextFixer))
                {
                    if (existingTamilTextFixer.hideFlags == HideFlags.HideAndDontSave)
                    {
                        tamilTextFixers.Add(existingTamilTextFixer);
                    }

                    return;
                }

                var tamilTextFixer = tmpText.gameObject.AddComponent<TamilTextFixer>();
                tamilTextFixer.hideFlags = HideFlags.HideAndDontSave;

                tamilTextFixers.Add(tamilTextFixer);
            }
        }

        public static void RemoveFromAutoAssignedList(TamilTextFixer tamilTextFixer)
        {
            if (tamilTextFixers.Contains(tamilTextFixer))
            {
                tamilTextFixers.Remove(tamilTextFixer);
            }
        }
    }
}