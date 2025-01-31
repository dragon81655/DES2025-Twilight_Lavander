using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
public class TransitionFSM : ScriptableObject
{
    [SerializeField]
    private List<ConditionFSM> conditions;

    [SerializeField]
    private StateFSM targetState;

    public bool CheckConditions(FSMController fsm)
    {
        if(conditions != null)
        {
            int falseConditions = 0;
            foreach(ConditionFSM condition in conditions)
            {
                //Basically it adds mustBeTrue to falseConditions so it returns false if all conditions that aren't mustBeTrue return false, 
                //it returns false too. So if 1 mustBeTrue returns false, it's false or if ALL must not be true return false, then it's false.
                //It's integrating a AND and OR conditions in a AND inside the same list.
                if (!condition.CheckCondition(fsm))
                {
                    if (condition.mustBeTrue) return false;
                    falseConditions++;
                    if(conditions.Count <= falseConditions) return false;
                }
                else
                {
                    if(condition.mustBeTrue) falseConditions++;
                }
            }
        }
        return true;
    }
    public StateFSM GetTargetState()
    {
        return targetState;
    }
}
