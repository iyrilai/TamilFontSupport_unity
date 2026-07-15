using UnityEditor;
using UnityEngine;
using TamilUI;
using System;

namespace TamilUIEditor
{
    [Obsolete]
    [CustomEditor(typeof(TamilDropdown), true)]
    public class TamilDropdownEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TamilDropdown tamilDropdown = (TamilDropdown)target;
            if (GUILayout.Button("Render Text"))
            {
                tamilDropdown.Start();
            }
        }
    }
}