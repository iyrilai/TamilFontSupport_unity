using UnityEngine;
using TMPro;


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

        private static void OnPlayModeChanged(PlayModeStateChange state)
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

        static void Cleanup()
        {
            TamilFontFixerSettings settings = TamilFontFixerSettings.Get();
            if (settings == null)
            {
                return;
            }

            if (settings.DynamicallyLoadOnEditor)
            {
                return;
            }

            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(OnTextChanged);
            isInitialized = false;
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
                if (tmpText.TryGetComponent<TamilTextFixer>(out _))
                {
                    return;
                }

                var tamilTextFixer = tmpText.gameObject.AddComponent<TamilTextFixer>();
                tamilTextFixer.hideFlags = HideFlags.DontSave;
            }
        }
    }
}