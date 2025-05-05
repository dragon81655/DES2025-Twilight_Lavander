using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseColorBlind : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private InputDataStaticClass settings;
    public void ChooseMode(int mode)
    {
        PlayerPrefs.SetInt("ColorBlindMode", mode);
        PlayerPrefs.Save();
        Debug.Log("Mode: " + mode);
        text.text = "Selected: " + ((ColorBlindMode)mode).ToString();
    }
}
