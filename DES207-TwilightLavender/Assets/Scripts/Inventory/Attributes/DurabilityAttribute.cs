using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityAttribute : AttributeBase
{
    public int durability;
    public int maxDurability;

    public override void Init()
    {
    }

    public override AttributeBase Copy()
    {
        DurabilityAttribute at = new DurabilityAttribute();
        at.durability = durability;
        at.maxDurability= maxDurability;
        at.attributeName = attributeName;
        return at;
    }
}
