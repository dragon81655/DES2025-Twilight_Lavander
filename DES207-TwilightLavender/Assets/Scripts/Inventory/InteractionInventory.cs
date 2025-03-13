using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionInventory : MonoBehaviour, IInteractable
{
    InventoryController inventoryController;
    public void Interact(GameObject source)
    {
        InventoryController ic = source.GetComponent<InventoryController>();
        if(inventoryController != null && ic != null)
        {
            List<Item> leftOvers = (List<Item>)ic.AddItemRange(inventoryController.GetInventoryCopy());
            if (leftOvers.Count > 0)
            {
                inventoryController.ClearInventory();
                inventoryController.AddItemRange(leftOvers);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
