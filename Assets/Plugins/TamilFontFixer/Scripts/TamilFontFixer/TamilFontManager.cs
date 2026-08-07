using System.Collections.Generic;
using TMPro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Iyrilai.TamilFontFixer
{
    static class TamilFontManager
    {
        static Dictionary<string, TMP_FontAsset> TamilFontAssets { get; } = new Dictionary<string, TMP_FontAsset>();

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void EditorInitializeOnLoadMethod()
        {
            TMP_Text.OnFontAssetRequest += OnFontRequested;
        }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void RuntimeInitializeOnLoadMethod()
        {
            TMP_Text.OnFontAssetRequest += OnFontRequested;
        }

        public static void RegisterTamilFontAsset(TMP_FontAsset fontAsset)
        {
            if (fontAsset != null)
            {
                string fontHashCode = fontAsset.name;

                if (!TamilFontAssets.ContainsKey(fontHashCode))
                {
                    TamilFontAssets.Add(fontHashCode, fontAsset);
                }
            }
        }

        static TMP_FontAsset OnFontRequested(int fontHashCode, string fontName)
        {
            if (TamilFontAssets.TryGetValue(fontName, out TMP_FontAsset font))
            {
                return font;
            }

            return null;
        }
    }
}