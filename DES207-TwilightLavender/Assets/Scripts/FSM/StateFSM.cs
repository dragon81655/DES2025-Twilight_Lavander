using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FSM/State")]
public class StateFSM : ScriptableObject
{
    [SerializeField]
    private List<ActionFSM> entryActions;
    [SerializeField]
    private List<ActionFSM> actions;
    [SerializeField]
    private List<ActionFSM> exitActions;

    [SerializeField]
    private List<TransitionFSM> transitionFSMs;

    public void RunEntryActions(FSMController fsm)
    {
        foreach(ActionFSM action in entryActions)
        {
            action.Act(fsm);
        }
    }

    public void RunActions(FSMController fsm)
    {
        foreach (ActionFSM action in actions)
        {
            action.Act(fsm);
        }
    }

    public void RunExitActions(FSMController fsm)
    {
        foreach (ActionFSM action in exitActions)
        {
            action.Act(fsm);
        }
    }

    public StateFSM CheckTransitions(FSMController fsm)
    {
        foreach(TransitionFSM t in transitionFSMs)
        {
            if (t.CheckConditions(fsm))
            {
                return t.GetTargetState();
            }
        }
        return null;
    }
}
