using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image BarBack; // grabbing bar back
    public Image BarFill; // grabbing bar fill
    public Image BarLine; // grabbing bar line

    public TextMeshProUGUI TimerText; // grabbing timer text

    public GameStateManager GameStateManager; // grabbing gamestatemanager script for time
    public SFXManager SFXManager; // for bar flip

    // Start is called before the first frame update
    void Start()
    {
        float switchTimerB = GameStateManager.instance.GetEndGameTimer(); // setting endgame timer to gamestatemanager declared value
        Debug.Log("Endgame Timer: " + switchTimerB); // for testing

        float switchTimerValueB = GameStateManager.instance.GetCurrentEndGameTimer(); // setting current endgame timer to gamestatemanager declared value
        Debug.Log("Current Endgame Timer: " + switchTimerValueB); // for testing

        float switchTimerA = GameStateManager.instance.GetSwitchTimer(); // for switch
        Debug.Log("Switch Timer: " + switchTimerA); // for testing

        float switchTimerValueA = GameStateManager.instance.GetCurrentSwitchTimer(); // for switch
        Debug.Log("Switch Timer: " + switchTimerValueA); // for testing
    }

    // Update is called once per frame
    void Update()
    {
        float switchTimerValueB = GameStateManager.instance.GetCurrentEndGameTimer(); // updating current switch time for bar
        float switchTimerB = GameStateManager.instance.GetEndGameTimer(); // updating total time for bar

        float switchTimerValueA = GameStateManager.instance.GetCurrentSwitchTimer(); // updating current switch time for line
        float SwitchTimerA = GameStateManager.instance.GetSwitchTimer(); // updating total switch time for line

        if (switchTimerValueB > 0)
        {
            BarFill.fillAmount = 1f - (switchTimerValueB / switchTimerB); // bar filling from 0 to 1, right to left
        }
        else
        {
            BarFill.fillAmount = 0f; // bar full
        }

        float barWidth = ((RectTransform)BarBack.transform).rect.width;

        float switchProgress = Mathf.Clamp01(switchTimerValueA / SwitchTimerA);

        float positionX = switchProgress * barWidth - (barWidth / 2f);

        Vector2 newLinePos = BarLine.rectTransform.anchoredPosition;
        newLinePos.x = positionX;
        BarLine.rectTransform.anchoredPosition = newLinePos;


        int minutes = Mathf.FloorToInt(switchTimerValueB / 60); // converting to mm:ss
        int seconds = Mathf.FloorToInt(switchTimerValueB % 60); // converting to mm:ss
        TimerText.text = $"{minutes:00}:{seconds:00}"; // displaying timer in mm:ss
    }
}
