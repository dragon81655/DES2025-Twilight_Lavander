using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class StatusEffectsController : MonoBehaviour
{
    [SerializeField]
    private List<StatusEffectsBase> effects;

    public Dictionary<string, object> SECDictionary= new Dictionary<string, object>();
    
    public void AddEffect(StatusEffectsBase effect, GameObject source)
    {
        effects.Add(effect);
        effect.StartEffect(this, source);
    }

    public void RemoveEffect(StatusEffectsBase effect)
    {
        effects.Remove(effect);
        effect.StopEffect(this);
    }

    public void RemoveEffectsFromSource(GameObject source)
    {
        effects.RemoveAll(s => s.GetSource() == source);
    }

    public void RemoveAllEffectsOfTypoe(Type type)
    {
        effects.RemoveAll(s => s.GetType() == type);
    }

    private void Update()
    {
        foreach (StatusEffectsBase effect in effects) 
        {
            effect.Tick(this);
        }
    }
}
