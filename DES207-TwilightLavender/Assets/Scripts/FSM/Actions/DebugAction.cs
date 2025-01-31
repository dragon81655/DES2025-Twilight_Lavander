using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Debug")]

public class DebugAction : ActionFSM
{
    [SerializeField] private string toSay;
    public override void Act(FSMController fsm)
    {
       Debug.Log(toSay);
    }
}
