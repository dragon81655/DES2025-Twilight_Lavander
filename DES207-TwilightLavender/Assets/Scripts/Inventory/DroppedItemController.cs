using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemController : InventoryController, ITestInteractable
{
    public void Interact(GameObject source)
    {
        InventoryController ic = source.GetComponent<InventoryController>();
        if (ic)
        {
            List<Item> leftOvers =(List<Item>)ic.AddItemRange(inventory);
            if(leftOvers.Count > 0)
            {
                inventory = leftOvers;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
