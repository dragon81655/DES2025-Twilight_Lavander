using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Conditions/Debug")]
public class DebugCondition : ConditionFSM
{
    [SerializeField] private bool returnValue;
    public override bool CheckCondition(FSMController fsm)
    {
        return returnValue;
    }
}
