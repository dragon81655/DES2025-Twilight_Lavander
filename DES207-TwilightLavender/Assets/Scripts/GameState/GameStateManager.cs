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
    [SerializeField]
    private float currentSwitchTimer;

    [SerializeField] private float endGameTimer;
    private float currentEndGameTimer;


    [Header("Events")]
    [SerializeField] private UnityEvent onHumanWin; 
    [SerializeField] private UnityEvent onVirusWin; 
    private bool runTimer = false;
    private bool switchTimerCheck = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        runTimer = true;
        switchTimerCheck = true;
        currentEndGameTimer = endGameTimer;
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

    public void PauseSwitchTimer()
    {
        switchTimerCheck = false;
    }

    public void ContinueSwitchTimer()
    {
        switchTimerCheck = true;
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
            if (switchTimerCheck)
            {
                currentSwitchTimer -= t;
            }
            if(currentEndGameTimer <= 0)
            {
                FinishGame(EndGameStatus.VirusWin);
            }
            if(currentSwitchTimer <= 0)
            {
                if (InputManager.instance != null)
                {
                    InputManager.instance.RequestSwitchChars();
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
            onVirusWin.Invoke();
        }else if(status == EndGameStatus.HumanWin)
        {
            EndGamePortal[] portals = (EndGamePortal[])Resources.FindObjectsOfTypeAll(typeof(EndGamePortal));
            foreach(EndGamePortal portal in portals)
            {
                portal.SwitchPortalState(true);
            }
            StopTimer();
            onHumanWin.Invoke();
        }else
        {
            Debug.Log("Tough luck, you both suck!");
            
        }
    }

    public float GetCurrentSwitchTimer() // for UI depletion bar
    {
        return currentSwitchTimer;
    }

    public float GetSwitchTimer() // for UI depletion bar
    {
        return switchTimer;
    }
}

[Serializable]
public enum EndGameStatus
{
    VirusWin = 0,
    HumanWin = 1,
    Draw = 2
}
