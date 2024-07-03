using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TamilUI;

public class UpdateTamilText : MonoBehaviour
{
    [SerializeField] TamilText tamilText;

    int i = 0;
    public void OnButtonPressed()
    {
        string[] tamilLetter = { "து மை காசு", "வணக்கம்", "நன்றி" };

        if (i == 3)
            i = 0;

        tamilText.Text = tamilLetter[i];
        i++;
    }
}
