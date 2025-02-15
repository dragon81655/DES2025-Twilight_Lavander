using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected List<Item> inventory = new List<Item>();
    protected List<Item> tickable = new List<Item>();

    [SerializeField] protected int slotAmount = 0;
    [Tooltip("If the inventory is locked, the name can be used to identify if key X is for this chest + you can give it the name")]
    [SerializeField] protected string inventoryName;

    private void Awake()
    {
        List<Item> toDelete = new List<Item>();
        foreach(Item i in inventory)
        {
            if(!i.Init(this)){
                toDelete.Add(i);
            }
        }
        foreach (Item i in toDelete) {
            RemoveItem(i);
        }
    }
    public int GetSlotAmount() { return slotAmount; }
    public List<Item> GetInventoryCopy()
    {
        List<Item> toReturn = new List<Item>();
        foreach (Item item in inventory)
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
            if (item2.GetId() == item.GetId())
            { 
                //This trys to add the amount in item to item2, mixing them in the same slot.
                if (item2.AddAmountWithItem(item)) return true;
            }
        }
        if (inventory.Count < slotAmount)
        {
            item.UpdateController(this);
            inventory.Add(item);
            return true;
        }
        return false;
    }


    public bool AddItem(ItemBase item)
    {
        Item t = new Item(item);
        t.UpdateController(this);
        return AddItem(t);
    }
    public bool AddItem(ItemBase item, int slot)
    {
        Item t = new Item(item);
        t.UpdateController(this);
        return AddItem(t, slot);
    }
    public bool AddItem(Item item, int slot)
    {
        if (slot >= inventory.Count || slot >= slotAmount) return false;

        if (inventory[slot] == null)
        {
            item.UpdateController(this);
            inventory[slot] = item;
            return true;
        }

        if (inventory[slot].GetId() == item.GetId())
        {
            return inventory[slot].AddAmountWithItem(item);
        }
        return false;
    }
    public Item GetItem(int slot)
    {
        if (inventory.Count > slot)
        return inventory[slot];
        else return null;
    }
    public Item[] GetItemByName(string itemName)
    {
        List<Item> list = new List<Item>();
        foreach(Item i in inventory)
        {
            if(i.GetItemName() == itemName)
            list.Add(i);
        }
        return list.ToArray();
    }
    public IEnumerable<Item> AddItemRange(IEnumerable<Item> items)
    {
        List<Item> toReturn = new List<Item>();
        foreach(Item item in items)
        {
            if (!AddItem(item))
            {
                toReturn.Add(item);
            }
        }
        return toReturn;
    }

    public void TakeItemAmount(int id, int amount)
    {
        int amountLeft = amount;
        foreach (Item item2 in inventory)
        {
            if (item2.GetId() == id)
            {
                int t = item2.GetAmount();
                item2.TakeAmount(amountLeft);
                if (amountLeft - t <= 0) return;
                amountLeft -= t;
            }
        }
    }

    public void TakeItemAmount(string itemName, int amount)
    {
        int amountLeft = amount;
        foreach (Item item2 in inventory)
        {
            if (item2.GetItemName().Equals(itemName))
            {
                int t = item2.GetAmount();
                item2.TakeAmount(amountLeft);
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

    public bool HasItem(string itemName, int amount)
    {
        int remainingAmount = amount;
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetItemName().Equals(itemName))
            {
                remainingAmount -= inventory[i].GetAmount();
                if(remainingAmount <= 0) return true;
            }
        }
        return false;
    }
    public bool HasItemByTag(string tag, int amount)
    {
        int remainingAmount = amount;
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].tags == null) return false;
            for(int j = 0; j < inventory[i].tags.Count; j++)
            {
                if (inventory[i].tags[j].Equals(tag))
                {
                    remainingAmount -= inventory[i].GetAmount();
                    if (remainingAmount <= 0) return true;
                }
            }
        }
        return false;
    }

    public bool HasItem(ItemBase itemBase, int amount)
    {
        int remainingAmount = amount;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetId() == itemBase.id)
            {
                remainingAmount -= inventory[i].GetAmount();
                if (remainingAmount <= 0) return true;
            }
        }
        return false;
    }

    public bool CheckItemAmount(int id, int amount)
    {
        int amountLeft = amount;
        foreach (Item item in inventory)
        {
            if (item.GetId() == id)
            {
                amountLeft -= item.GetAmount();
            }
            if (amountLeft <= 0) return true;
        }
        return false;
    }

    public bool CheckItemAmount(string itemName, int amount)
    {
        int amountLeft = amount;
        foreach (Item item in inventory)
        {
            if (item.GetItemName().Equals(itemName))
            {
                amountLeft -= item.GetAmount();
            }
            if (amountLeft <= 0) return true;
        }
        return false;
    }

    public bool CanAddItem(Item item)
    {
        foreach (Item item2 in inventory)
        {
            if (item2.GetAmount() == item2.GetMaxAmount()) continue;
            if (item2.GetId() == item.GetId())
            {
                //This trys to add the amount in item to item2, mixing them in the same slot.
                if (item2.CanAddAmountWithItem(item)) return true;
            }
        }
        if (inventory.Count < slotAmount)
        {
            return true;
        }
        return false;
    }

    protected void Update()
    {
        for(int i = 0; i < tickable.Count; i++)
        {
            tickable[i].Tick();
        }
    }

    private void OnValidate()
    {
        foreach (Item i in inventory)
        {
            i.OnGUI();
        }
    }
}
