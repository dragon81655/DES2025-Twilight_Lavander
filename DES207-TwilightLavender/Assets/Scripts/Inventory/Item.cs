using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField]
    private ItemBase itemBase = null;
    private ItemBase currentItemBase = null;

    [SerializeField]private string displayName;
    [SerializeField] private int amount;

    public List<string> tags;

    public AttributeControler attributes;

    public Dictionary<string, object> data = new Dictionary<string, object>();

    //ItemBase is a scriptable object to create the bases of the item, so it's easier for designers. Items with custom behaviour are childs of this class as well.
    

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
    public int GetAmount()
    {
        return amount;
    }

    public int GetMaxAmount()
    {
        return itemBase.maxAmount;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public void SetDisplayName(string name)
    {
        displayName = name;
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

    public AttributeControler GetAttributes()
    {
        return attributes;
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
    public void OnGUI()
    {
        if(currentItemBase != itemBase)
        {
            currentItemBase = itemBase;
            UpdateOnItemBase(currentItemBase);
        }
    }

    private void UpdateOnItemBase(ItemBase itemBase)
    {
        if (itemBase == null) return;
        this.itemBase = itemBase;
        this.displayName = itemBase.itemName;
        tags = new List<string>();
        tags.AddRange(itemBase.tags);
        if(attributes == null)
        attributes = new AttributeControler();
        attributes.Concatonate(itemBase.attributes.Copy());
       
    }

    public Item(ItemBase itemBase)
    {
        UpdateOnItemBase(itemBase);
    }
    public Item(Item item)
    {
        this.amount = item.amount;
        this.itemBase = item.itemBase;
        displayName= item.displayName;
        tags= item.tags;
        attributes = item.attributes;
    }
}
