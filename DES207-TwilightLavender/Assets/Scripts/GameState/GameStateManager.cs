using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [Header("Timers")]
    [SerializeField] private float switchTimer;
    private float currentSwitchTimer;

    [SerializeField] private float endGameTimer;
    private float currentEndGameTimer;


    [Header("Events")]
    [SerializeField] private UnityEvent onHumanWin; 
    [SerializeField] private UnityEvent onVirusWin; 
    [SerializeField] private UnityEvent onSwitch; 
    private bool runTimer = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        runTimer = true;
        currentEndGameTimer= endGameTimer;
        currentSwitchTimer = switchTimer;
    }
    public void StopTimer()
    {
        runTimer = false;
    }
    public void ContinueTimer()
    {
        runTimer = true;
    }
    public void AddSwitchTimer(float value)
    {
        currentSwitchTimer = Mathf.Clamp(Mathf.Abs(value) + currentSwitchTimer, 0, switchTimer);
    }
    public void TakeSwitchTimer(float value)
    {
        currentSwitchTimer = Mathf.Clamp(-Mathf.Abs(value) + currentSwitchTimer, 0, switchTimer);
    }

    public void AddEndGameTimer(float value)
    {
        currentEndGameTimer = Mathf.Clamp(Mathf.Abs(value) + currentEndGameTimer, 0, endGameTimer);
    }
    public void TakeEndGameTimer(float value)
    {
        currentEndGameTimer = Mathf.Clamp(-Mathf.Abs(value) + currentEndGameTimer, 0, endGameTimer);
    }
    private void Update()
    {
        if (runTimer)
        {
            float t = Time.deltaTime;
            currentEndGameTimer -= t;
            currentSwitchTimer -= t;
            if(currentEndGameTimer <= 0)
            {
                FinishGame(EndGameStatus.VirusWin);
            }
            if(currentSwitchTimer <= 0)
            {
                if (InputManager.instance != null)
                {
                    InputManager.instance.SwitchChars();
                }
                else Debug.LogError("No InputManager in scene!");
                currentSwitchTimer = switchTimer;
            }
        }
        
    }


    public void FinishGame(EndGameStatus status)
    {
        if(status == EndGameStatus.VirusWin)
        {
            Debug.Log("Virus wins!");
        }else if(status == EndGameStatus.HumanWin)
        {
            EndGamePortal[] portals = (EndGamePortal[])Resources.FindObjectsOfTypeAll(typeof(EndGamePortal));
            foreach(EndGamePortal portal in portals)
            {
                portal.SwitchPortalState(true);
            }
        }else
        {
            Debug.Log("Tough luck, you both suck!");
        }
    }

    public float GetCurrentSwitchTimer() // for golden line
    {
        return currentSwitchTimer;
    }

    public float GetSwitchTimer() // for golden line
    {
        return switchTimer;
    }

    public float GetCurrentEndGameTimer() // for takeover bar
    {
        return currentEndGameTimer;
    }

    public float GetEndGameTimer() // for takeover bar
    {
        return endGameTimer;
    }
}

[Serializable]
public enum EndGameStatus
{
    VirusWin = 0,
    HumanWin = 1,
    Draw = 2
}
