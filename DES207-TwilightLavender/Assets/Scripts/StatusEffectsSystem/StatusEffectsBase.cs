using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffectsBase : ScriptableObject
{
    protected GameObject source;

    public virtual GameObject GetSource(){
        return source;
    }

    public abstract void Tick(StatusEffectsController controller);

    public abstract void StartEffect(StatusEffectsController controller, GameObject source);

    public abstract void StopEffect(StatusEffectsController controller);
}
