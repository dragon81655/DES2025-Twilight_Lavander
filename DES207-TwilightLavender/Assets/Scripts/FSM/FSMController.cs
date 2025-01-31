using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController : MonoBehaviour
{
    //Way for FSM scriptable objects to declare variables inside their controller without I declare them here, creating a mess.
    //+ since they are scriptable objects, I can reuse them between enemies, since they store stuff inside thir objects with this.
    public Dictionary<string, object> dictionaryFSM = new Dictionary<string, object>();

    [SerializeField]
    private StateFSM startingState;
    [SerializeField]
    private StateFSM currentState;
    void Start()
    {
        currentState = startingState;
        currentState.RunEntryActions(this);
    }

    void Update()
    {
        if (currentState == null)
        {
            Debug.LogError("Current state is null somehow");
            return;
        }
        StateFSM t = currentState.CheckTransitions(this);
        if (t == null)
        {
            currentState.RunActions(this);
        }
        else
        {
            currentState.RunExitActions(this);
            currentState = t;
        }
    }
}
