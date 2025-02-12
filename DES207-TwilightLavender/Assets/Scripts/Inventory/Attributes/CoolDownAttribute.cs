using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownAttribute : AttributeBase
{
    public float timer;
    public override void Init(InventoryController controller, Item item)
    {
        
    }
    public override AttributeBase Copy()
    {
        CoolDownAttribute at = new CoolDownAttribute();
        at.timer = timer;
        at.attributeName= attributeName;
        return at;
    }

    public override void RunAttribute(InventoryController controller, Item item)
    {
    }

    public override void TickAttribute(InventoryController controller, Item item)
    {
    }
}
