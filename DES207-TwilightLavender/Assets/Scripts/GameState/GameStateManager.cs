using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    private void Awake()
    {
        instance = this;
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
}

[Serializable]
public enum EndGameStatus
{
    VirusWin = 0,
    HumanWin = 1,
    Draw = 2
}
