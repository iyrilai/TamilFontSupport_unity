using Iyrilai.TamilEncoder;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace Iyrilai.TamilFontFixer
{
    public class TamilFontFixerSettings : ScriptableObject
    {
        [SerializeField] TMP_FontAsset fontAsset;
        [SerializeField] TamilFontEncoding defaultEncoding = TamilFontEncoding.TACE16;

        [SerializeField] bool dynamicallyLoadAddTamilText = false;
        [SerializeField] bool dynamicallyLoadOnEditor = false;

        public TMP_FontAsset FontAsset => fontAsset;
        public TamilFontEncoding DefaultEncoding => defaultEncoding;

        public bool DynamicallyLoadAddTamilText => dynamicallyLoadAddTamilText;
        public bool DynamicallyLoadOnEditor => dynamicallyLoadOnEditor;

        const string ResourcePath = "TamilFontFixerSettings";
        const string ResourcesFolder = "Assets/Resources";
        const string AssetPath = ResourcesFolder + "/TamilFontFixerSettings.asset";

        static TamilFontFixerSettings cachedInstance;

        void OnValidate()
        {
            AutoTamilTextFixerAssigner.RestartInitialize();
        }

        public static TamilFontFixerSettings Get()
        {
            if (cachedInstance != null)
                return cachedInstance;

            cachedInstance = Resources.Load<TamilFontFixerSettings>(ResourcePath);

#if UNITY_EDITOR
            if (cachedInstance == null)
            {
                if (!AssetDatabase.IsValidFolder(ResourcesFolder))
                {
                    Directory.CreateDirectory(ResourcesFolder);
                    AssetDatabase.Refresh();
                }

                cachedInstance = CreateInstance<TamilFontFixerSettings>();
                AssetDatabase.CreateAsset(cachedInstance, AssetPath);
                AssetDatabase.SaveAssets();

                Debug.Log($"[TamilFontFixerSettings] Created new settings asset at '{AssetPath}'.");
            }
#else
            if (cachedInstance == null)
            {
                Debug.LogWarning($"[TamilFontFixerSettings] No settings asset found. " +
                                   "This should have been created & assigned value in the editor before building.");
            }
#endif

            return cachedInstance;
        }
    }
}