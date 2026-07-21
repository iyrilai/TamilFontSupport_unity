using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iyrilai.TamilEncoder;
using TMPro;
using UnityEngine;

namespace Iyrilai.TamilFontFixer
{
    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("UI/Tamil Text Fixer")]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class TamilTextFixer : MonoBehaviour, ITextPreprocessor
    {
        [SerializeField] bool overrideSetting;
        [SerializeField] TMP_FontAsset fontAsset;
        [SerializeField] TamilFontEncoding defaultEncoding;

        bool deactivated = false;
        TMP_Text tmp_text;
        TamilFontFixerSettings settings;

        #region Public Properties

        public TMP_Text TextMesh => tmp_text;

        public bool Deactivated
        {
            get
            {
                return deactivated;
            }
            set
            {
                deactivated = value;

                if (deactivated)
                {
                    Disable();
                }
                else
                {
                    Initialize();
                }
            }
        }

        public bool OverrideSetting
        {
            get { return overrideSetting; }
            set
            {
                overrideSetting = value;
                ReloadTextMesh();
            }
        }

        public TMP_FontAsset OverrideFontAsset
        {
            get { return fontAsset; }
            set
            {
                fontAsset = value;
                ReloadTextMesh();
            }
        }

        public TamilFontEncoding OverrideDefaultEncoding
        {
            get { return defaultEncoding; }
            set
            {
                defaultEncoding = value;
                ReloadTextMesh();
            }
        }

        #endregion

        #region Unity Callbacks

        void Awake()
        {
            Initialize();
        }

        void OnValidate()
        {
            if (!gameObject.activeInHierarchy || !enabled)
                return;

            Initialize(true);
        }

        void OnDestroy()
        {
            Disable();
        }

        #endregion

        #region Activate and Deactivate

        void Initialize(bool isEditor = false)
        {
            if (deactivated)
                return;

            tmp_text = GetComponent<TMP_Text>();
            tmp_text.textPreprocessor = this;

            settings = TamilFontFixerSettings.Get();

            TMP_Text.OnFontAssetRequest -= OnFontRequested;
            TMP_Text.OnFontAssetRequest += OnFontRequested;

            DelayMeshUpdate(isEditor);
        }

        void Disable()
        {
            TMP_Text.OnFontAssetRequest -= OnFontRequested;

            if (tmp_text == null)
                return;

            tmp_text.textPreprocessor = null;
            ReloadTextMesh();
        }

        void DelayMeshUpdate(bool isEditor)
        {
            if (!isEditor)
            {
                ReloadTextMesh();
                return;
            }

#if UNITY_EDITOR
            UnityEditor.EditorApplication.delayCall += ReloadTextMesh;
#else
            ReloadTextMesh();
#endif
        }

        #endregion

        #region Internal Getters

        TMP_FontAsset GetFontAsset()
        {
            if (overrideSetting)
            {
                return fontAsset;
            }
            else
            {
                if (settings == null)
                {
                    Debug.LogWarning("[TamilTextFixer] Tamil Font Fixer Settings not found. Please create a settings asset in the Resources folder.");
                    return null;
                }

                return settings.FontAsset;
            }
        }

        TamilFontEncoding GetEncoding()
        {
            if (overrideSetting)
            {
                return defaultEncoding;
            }
            else
            {
                if (settings == null)
                {
                    Debug.LogWarning("[TamilTextFixer] Tamil Font Fixer Settings not found. Please create a settings asset in the Resources folder.");
                    return TamilFontEncoding.TACE16;
                }

                return settings.DefaultEncoding;
            }
        }

        #endregion

        void ReloadTextMesh()
        {
            if (tmp_text == null) // fail safe
                return;

            tmp_text.ForceMeshUpdate();
        }

        string ITextPreprocessor.PreprocessText(string text)
        {
            var font = GetFontAsset();

            if (font == null)
            {
                Debug.LogWarning("[TamilTextFixer] Font Asset is not assigned.");
                return text;
            }

            var encoding = GetEncoding();
            var encodedText = TamilEncoding.ConvertFromUnicode(text, encoding);

            encodedText = AddFontTags(encodedText, TamilEncoding.GetCharSet(encoding), font);
            return encodedText;
        }

        public static string AddFontTags(string input, string[] keywords, TMP_FontAsset fontAsset)
        {
            if (string.IsNullOrEmpty(input) || keywords == null || keywords.Length == 0)
                return input;

            var ranges = new List<(int Start, int End)>();

            foreach (var keyword in keywords)
            {
                if (string.IsNullOrEmpty(keyword)) continue;

                int index = 0;
                while ((index = input.IndexOf(keyword, index, StringComparison.Ordinal)) != -1)
                {
                    ranges.Add((index, index + keyword.Length));
                    index += 1;
                }
            }

            if (ranges.Count == 0)
                return input;

            ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

            var merged = new List<(int Start, int End)>();
            var current = ranges[0];

            foreach (var range in ranges.Skip(1))
            {
                if (range.Start <= current.End)
                {
                    current.End = Math.Max(current.End, range.End);
                }
                else
                {
                    string gap = input.Substring(current.End, range.Start - current.End);
                    if (gap.Length > 0 && gap.Trim().Length == 0)
                    {
                        current.End = Math.Max(current.End, range.End);
                    }
                    else
                    {
                        merged.Add(current);
                        current = range;
                    }
                }
            }

            merged.Add(current);

            var result = new StringBuilder();
            int lastPos = 0;

            foreach (var range in merged)
            {
                result.Append(input, lastPos, range.Start - lastPos);
                result.Append($"<font=\"{fontAsset.name}\">");
                result.Append(input, range.Start, range.End - range.Start);
                result.Append("</font>");
                lastPos = range.End;
            }

            result.Append(input, lastPos, input.Length - lastPos);

            return result.ToString();
        }

        TMP_FontAsset OnFontRequested(int fontHashCode, string fontName)
        {
            var font = GetFontAsset();

            if (font == null)
            {
                Debug.LogWarning("[TamilTextFixer] Font Asset is not assigned.");
                return null;
            }

            if (fontHashCode == font.hashCode)
            {
                return font;
            }

            if (fontName == font.name)
            {
                return font;
            }

            return null;
        }
    }
}