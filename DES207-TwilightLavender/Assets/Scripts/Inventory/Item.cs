using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int durability;
    private int amount;

    public List<string> tags;

    //ItemBase is a scriptable object to create the bases of the item, so it's easier for designers. Items with custom behaviour are childs of this class as well.
    private ItemBase itemBase;

    public int GetId()
    {
        return itemBase.GetId();
    }
    public string GetItemName()
    {
        return itemBase.itemName;
    }
    
    public Sprite GetItemSprite()
    {
        return itemBase.sprite;
    }

    public int GetDurability()
    {
        return durability;
    }
    public int GetMaxDurability()
    {
        return itemBase.maxDurability;
    }

    public int GetAmount()
    {
        return amount;
    }

    public int GetMaxAmount()
    {
        return itemBase.maxAmount;
    }
    public bool IsTickable()
    {
        return itemBase.isTickable;
    }
    public void TakeAmount(int amount)
    {
        this.amount = Mathf.Clamp(this.amount - amount, 0, GetMaxAmount());
    }

    public void AddAmount(int amount)
    {
        this.amount = Mathf.Clamp(this.amount + amount, 0, GetMaxAmount());
    }

    public bool AddAmountWithItem(Item item)
    {
        if(GetId() != item.GetId()) return false;
        if (GetAmount() + item.GetAmount() <= GetMaxAmount())
        {
            AddAmount(item.GetAmount());
            return true;
        }
        else
        {
            int toAdd = GetMaxAmount() - GetAmount();
            AddAmount(toAdd);
            item.TakeAmount(toAdd);
        }
        return false;
    }

    public void TakeDurability(int amount)
    {
        this.durability = Mathf.Clamp(this.durability - amount, 0, GetMaxDurability());
    }

    public void AddDurability(int amount)
    {
        this.durability = Mathf.Clamp(this.durability + amount, 0, GetMaxDurability());
    }
    
    public  void Use(InventoryController controller)
    {
        itemBase.Use(controller, this);
    }
    public  bool CanUse(InventoryController controller)
    {
        return itemBase.CanUse(controller, this);
    }
    public  void OnCreateItem(InventoryController controller)
    {
        itemBase.OnCreateItem(controller, this);
    }
    public  void OnDestroyItem(InventoryController controller)
    {
        itemBase.OnDestroyItem(controller, this);
    }
    public void Tick(InventoryController controller)
    {
        itemBase.Tick(controller, this);
    }
    public Item(ItemBase itemBase)
    {
        this.itemBase = itemBase;
        tags = new List<string>();
        tags.AddRange(itemBase.tags);
    }
    public Item(Item item)
    {
        this.amount = item.amount;
        this.durability = item.durability;
        this.itemBase = item.itemBase;
        tags= item.tags;
    }
}
