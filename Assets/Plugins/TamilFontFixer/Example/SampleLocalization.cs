using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Iyrilai.TamilFontFixer.Example
{
    public class SampleLocalization : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] TMP_Dropdown languageDropdown;
        [SerializeField] Button toggleBtn;
        [SerializeField] TextMeshProUGUI toggleBtnText;
        [SerializeField] TextMeshProUGUI localizeTargetText;

        enum Language { English, Tamil }
        Language currentLanguage = Language.English;

        bool btnTxtActive;

        void Start()
        {
            SetupDropdown();

            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
            toggleBtn.onClick.AddListener(OnToggleClicked);

            UpdateUI();
            OnToggleClicked();
        }

        void OnDestroy()
        {
            if (languageDropdown != null) languageDropdown.onValueChanged.RemoveListener(OnLanguageChanged);
            if (toggleBtn != null) toggleBtn.onClick.RemoveListener(OnToggleClicked);
        }

        void SetupDropdown()
        {
            languageDropdown.ClearOptions();

            List<string> options = new List<string> { "English", "தமிழ்" };
            languageDropdown.AddOptions(options);
        }

        void OnLanguageChanged(int index)
        {
            currentLanguage = (Language)index;
            UpdateUI();
        }

        void OnToggleClicked()
        {
            btnTxtActive = !btnTxtActive;
            
            if (btnTxtActive)
            {
                toggleBtnText.text = "செயலிழக்கச் செய்";
            }
            else
            {
                toggleBtnText.text = "செயல்படு";
            }
        }

        void UpdateUI()
        {
            if (currentLanguage == Language.Tamil)
            {
                localizeTargetText.text = "வணக்கம் டா மாப்ள unityல இருந்து";
            }
            else
            {
                localizeTargetText.text = "Hello! Bro, via Unity Editor";
            }
        }
    }
}