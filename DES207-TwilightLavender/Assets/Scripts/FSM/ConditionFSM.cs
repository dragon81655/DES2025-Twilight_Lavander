using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionFSM : ScriptableObject
{
    public bool mustBeTrue;
    public abstract bool CheckCondition(FSMController fsm);
}
