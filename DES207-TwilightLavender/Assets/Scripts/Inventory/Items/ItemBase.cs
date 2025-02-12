using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/BasicItem")]

public class ItemBase : ScriptableObject
{
    public int id;
    public string itemName;
    public string displayName;
    [TextArea(3,10)]
    public string description;
    public Sprite sprite;
    public int maxAmount;
    public bool isTickable;
    public List<string> tags;
    public AttributeControler attributes;


    public virtual void Use(InventoryController controller, Item item)
    {
        foreach(AttributeBase a in item.GetAttributes().list)
        {
            a.RunAttribute(controller, item);
        }
    }
    public virtual bool CanUse(InventoryController controller, Item item)
    {
        return true;
    }
    public virtual void OnCreateItem(InventoryController controller, Item item)
    {

    }
    public virtual void OnDestroyItem(InventoryController controller, Item item)
    {
        controller.RemoveItem(item);
    }
    public virtual void Tick(InventoryController controller, Item item)
    {
        if (!isTickable) return;
        foreach (AttributeBase a in item.GetAttributes().list)
        {
            a.TickAttribute(controller, item);
        }
    }

    public virtual void Init(InventoryController controller, Item item)
    {
        foreach(AttributeBase a in item.GetAttributes().list)
        {
            a.Init(controller, item);
        }
    }
    public int GetId()
    {
        return id;
    }
}
