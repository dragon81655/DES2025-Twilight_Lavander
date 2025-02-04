using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemBase
{
    [SerializeField] private float damage;
    [SerializeField] private int maxDurability;
    public override bool CanUse(InventoryController controller, Item item)
    {
        return true;
    }
    public override void OnCreateItem(InventoryController controller, Item item)
    {
        base.OnCreateItem(controller, item);
    }
}
