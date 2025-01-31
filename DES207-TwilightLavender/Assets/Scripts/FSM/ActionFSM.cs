using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionFSM : ScriptableObject
{
    public abstract void Act(FSMController fsm);
}
