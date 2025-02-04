using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownAttribute : AttributeBase
{
    public float timer;
    public override void Init()
    {
        
    }
    public override AttributeBase Copy()
    {
        CoolDownAttribute at = new CoolDownAttribute();
        at.timer = timer;
        at.attributeName= attributeName;
        return at;
    }
}
