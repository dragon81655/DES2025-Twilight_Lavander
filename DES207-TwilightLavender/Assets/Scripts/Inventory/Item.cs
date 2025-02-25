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

    private InventoryController controller;

    public List<string> tags;

    [SerializeField]
    private AttributeControler attributes;

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
    
    public GameObject GetDroppedModel()
    {
        return itemBase.droppedModel;
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

    public string GetDescription()
    {
        return itemBase.description;
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
        if(this.amount <= 0)
        {
            itemBase.OnDestroyItem(controller, this);
        }
    }

    public void AddAmount(int amount)
    {
        this.amount = Mathf.Clamp(this.amount + amount, 0, GetMaxAmount());
    }
    public bool CanAddAmountWithItem(Item item)
    {
        if (GetId() != item.GetId()) return false;
        if (GetAmount() + item.GetAmount() <= GetMaxAmount())
        {
            return true;
        }
        return false;
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
            int toAdd = Mathf.Clamp(GetMaxAmount() - GetAmount(), 0, GetMaxAmount());
            AddAmount(toAdd);
            item.TakeAmount(toAdd);
        }
        return false;
    }

    public AttributeControler GetAttributes()
    {
        return attributes;
    }
    public bool Init(InventoryController controller)
    {
        if(itemBase == null)
        {
            Debug.LogWarning($"The inventory Controller {controller.gameObject.name} has an Item without a ItemBase! Please remove or update them!");
            return false;
        }
        itemBase.Init(controller, this);
        this.controller = controller;
        return true;
    }
    public  void Use()
    {
        itemBase.Use(controller, this);
    }
    public  bool CanUse()
    {
        return itemBase.CanUse(controller, this);
    }
    public  void OnCreateItem()
    {
        itemBase.OnCreateItem(controller, this);
    }
    public  void OnDestroyItem()
    {
        itemBase.OnDestroyItem(controller, this);
    }
    public void Tick()
    {
        itemBase.Tick(controller, this);
    }
    public void OnGUI()
    {
        if(currentItemBase != itemBase)
        {
            UpdateOnItemBase(itemBase);
            currentItemBase = itemBase;
        }
    }

    public void UpdateController(InventoryController controller)
    {
        this.controller = controller;
    }

    private void UpdateOnItemBase(ItemBase itemBase)
    {
        if (itemBase == null) return;
        this.itemBase = itemBase;
        this.displayName = itemBase.itemName;
        tags = new List<string>();
        tags.AddRange(itemBase.tags);
        if(attributes == null || currentItemBase != null)
        attributes = new AttributeControler();
        attributes.Concatonate(itemBase.attributes.Copy());
       
    }

    public Item(ItemBase itemBase)
    {
        UpdateOnItemBase(itemBase);
    }
    public Item(ItemBase itemBase, int amount)
    {
        UpdateOnItemBase(itemBase);
        this.amount= amount;
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
