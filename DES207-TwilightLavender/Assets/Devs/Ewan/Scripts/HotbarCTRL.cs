using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarCTRL : MonoBehaviour
{
    public InventoryController inventoryController; // getting inventory script
    public GameObject hotbarSlotPrefab; // getting slot prefab
    public Transform hotbarContainer; // getting hotbar container
    public List<HotbarSlot> hotbarSlots = new List<HotbarSlot>(); // list of slots

    void Update()
    {
        //RefreshHotbar(); // calling to keep hotbar accurate
    }
    public void UpdateSlot(Item item, int index, bool selected)
    {
        if(index >= hotbarSlots.Count)
        {
            GameObject g = Instantiate(hotbarSlotPrefab, hotbarContainer);
            HotbarSlot slotComponent = g.GetComponent<HotbarSlot>();
            slotComponent.Initialize(item);
            hotbarSlots.Add(slotComponent);
        }
        if (item != hotbarSlots[index].item)
        {
            hotbarSlots[index].Initialize(item);
        }
        hotbarSlots[index].UpdateSlot();

        if (selected)
        {
            hotbarSlots[index].SetSelected(true);
            Debug.Log("Slot " + index + "   selected");
        }
        else
        {
            hotbarSlots[index].SetSelected(false);
            Debug.Log("Slot "+ index + " not selected");
        }
    }
    public void CheckDestroy(int currentMax)
    {
        if(currentMax < hotbarSlots.Count)
        {
            for (int i = currentMax; i < hotbarSlots.Count; i++)
            {
                Destroy(hotbarSlots[i].gameObject);
            }
            hotbarSlots.RemoveRange(currentMax, hotbarSlots.Count - currentMax);
        }
    }
    /*void RefreshHotbar() // function for keeping hotbar up to date
    {
        List<Item> items = inventoryController.GetInventoryCopy(); // grabbing current inventory

        int slotsToCreate = Mathf.Min(items.Count, inventoryController.GetSlotAmount()); // grabbing slot amount and limiting total slots

        for (int i = 0; i < slotsToCreate; i++) // iterate over inventory items
        {
            Item item = items[i];

            if (item.GetAmount() == 0) // if item quantity is 0
            {
                continue; // do not create a slot for this item
            }

            if (i < hotbarSlots.Count) // If a slot exists for this index, update it
            {
                HotbarSlot slotComponent = hotbarSlots[i].GetComponent<HotbarSlot>();
                if (slotComponent != null)
                {
                    slotComponent.Initialize(item); // update existing slot with new item info
                }
            }
            else // if no slot exists create one
            {
                GameObject newSlot = Instantiate(hotbarSlotPrefab, hotbarContainer); // create new slot
                HotbarSlot slotComponent = newSlot.GetComponent<HotbarSlot>(); // get slot details

                if (slotComponent != null) // if slot details empty
                {
                    slotComponent.Initialize(item); // initialize the slot with item data
                    hotbarSlots.Add(newSlot); // add new slot
                }
            }
        }

        for (int i = slotsToCreate; i < hotbarSlots.Count; i++) // remove any extra slots
        {
            Destroy(hotbarSlots[i]); // destroy the excess slot
        }

        
        hotbarSlots.RemoveRange(slotsToCreate, hotbarSlots.Count - slotsToCreate);
    }*/
}
