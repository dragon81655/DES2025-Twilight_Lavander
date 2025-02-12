using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurabilityAttribute : AttributeBase
{
    public int durability;
    public int maxDurability;

    public override void Init(InventoryController controller, Item item)
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

    public override void RunAttribute(InventoryController controller, Item item)
    {
        durability--;
        if(durability <=0)
        {
            item.TakeAmount(1);
            durability = maxDurability;
        }
    }
    
    public override void TickAttribute(InventoryController controller, Item item)
    {

    }
}
