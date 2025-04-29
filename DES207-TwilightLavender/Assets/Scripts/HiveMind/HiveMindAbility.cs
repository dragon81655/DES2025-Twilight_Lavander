using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class HiveMindAbility
{
    public GameObject source;

    public string abilityName;
    public string abilityDesc;
    public abstract void Tick(HiveMindController controller);
    public abstract void Init(HiveMindController controller);
    public abstract void Stop(HiveMindController controller);
    public abstract bool Act(HiveMindController controller);
}
