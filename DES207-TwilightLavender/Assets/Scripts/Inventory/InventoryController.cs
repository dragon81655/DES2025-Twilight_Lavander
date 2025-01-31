using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Item> inventory = new List<Item>();
    private List<Item> tickable = new List<Item>();

    [SerializeField] private int slotAmount = 0;
    [SerializeField] private bool isLocked;
    [Tooltip("If the inventory is locked, the name can be used to identify if key X is for this chest + you can give it the name")]
    [SerializeField] private string inventoryName;

    public List<Item> GetInventoryCopy()
    {
        List<Item> toReturn = new List<Item>();
        foreach(Item item in inventory)
        {
            toReturn.Add(new Item(item));
        }
        return toReturn;
    }

    public bool AddItem(Item item)
    {
        foreach (Item item2 in inventory)
        {
            if (item2.GetAmount() == item2.GetMaxAmount()) continue;
            if(item2.GetId() == item.GetId())
            {
                //This trys to add the amount in item to item2, mixing them in the same slot.
                if (item2.AddAmountWithItem(item)) return true;
            }
        }
        if(inventory.Count < slotAmount)
        {
            inventory.Add(item);
            return true;
        }
        return false;
    }
    public bool AddItem(Item item, int slot)
    {
        if(slot >= inventory.Count || slot >= slotAmount) return false;

        if (inventory[slot] == null)
        {
            inventory[slot] = item;
            return true;
        }

        if (inventory[slot].GetId() == item.GetId())
        {
            return inventory[slot].AddAmountWithItem(item);
        }
        return false;
    }

    public List<Item> AddItemRange(Item[] items)
    {
        List<Item> toReturn = new List<Item>();
        for(int i = 0; i < items.Length; i++)
        {
            if (!AddItem(items[i]))
            {
                toReturn.Add(items[i]);
            }
        }
        return toReturn;
    }

    public List<Item> AddItemRange(List<Item> items)
    {
        List<Item> toReturn = new List<Item>();
        for (int i = 0; i < items.Count; i++)
        {
            if (!AddItem(items[i]))
            {
                toReturn.Add(items[i]);
            }
        }
        return toReturn;
    }

    public bool CheckItemAmount(int id, int amount)
    {
        int amountLeft = amount;
        foreach(Item item in inventory)
        {
            if(item.GetId() == id)
            {
                amountLeft -= item.GetAmount();
            }
            if (amountLeft <= 0) return true;
        }
        return false;
    }

    public void TakeItemAmount(int id, int amount)
    {
        int amountLeft = amount;
        foreach (Item item in inventory)
        {
            if (item.GetId() == id)
            {
                int t = item.GetAmount();
                item.TakeAmount(amountLeft);
                if (amountLeft - t <= 0) return;
                amountLeft -= t;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
    }
    public void RemoveItem(int id)
    {
        foreach(Item item in inventory)
        {
            if(item.GetId() == id)
            {
                inventory.Remove(item);
                return;
            }
        }
    }
    public void RemoveAllItemsOfType(int id)
    {
        foreach(Item item in inventory)
        {
            if(item.GetId() == id) inventory.Remove(item);
        }
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }
    private void Update()
    {
        for(int i = 0; i < tickable.Count; i++)
        {
            tickable[i].Tick(this);
        }
    }
}
