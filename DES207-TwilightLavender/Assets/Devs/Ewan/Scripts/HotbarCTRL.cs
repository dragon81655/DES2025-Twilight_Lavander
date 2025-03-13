using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarCTRL : MonoBehaviour
{
    public InventoryController inventoryController; // getting inventory script
    public GameObject hotbarSlotPrefab; // getting slot prefab
    public Transform hotbarContainer; // getting hotbar container
    private List<GameObject> hotbarSlots = new List<GameObject>(); // list of slots

    void Update()
    {
        RefreshHotbar(); // calling to keep hotbar accurate
    }

    void RefreshHotbar() // function for keeping hotbar up to date
    {
        foreach (GameObject slot in hotbarSlots) // for every slot in hotbar
        {
            Destroy(slot); // destroy the slot
        }
        hotbarSlots.Clear(); // clear slot total

        List<Item> items = inventoryController.GetInventoryCopy(); // grabbing current inventory

        int slotsToCreate = Mathf.Min(items.Count, inventoryController.GetSlotAmount()); // grabbing slot amount and limiting total slots

        for (int i = 0; i < slotsToCreate; i++) // creating new slots
        {
            Item item = items[i];

            if (item.GetAmount() == 0) // if item quantity is 0
            {
                continue; // do not create a slot for this item
            }

            GameObject newSlot = Instantiate(hotbarSlotPrefab, hotbarContainer); // create new slot
            HotbarSlot slotComponent = newSlot.GetComponent<HotbarSlot>(); // get slot details

            if (slotComponent != null) // if slot details empty
            {
                slotComponent.Initialize(item); // call function for replacing slot info from HotbarSlot script
                hotbarSlots.Add(newSlot); // add new slot
            }
        }
    }
}
