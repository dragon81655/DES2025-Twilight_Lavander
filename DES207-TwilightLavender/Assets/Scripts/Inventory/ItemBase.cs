using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/BasicItem")]

public class ItemBase : ScriptableObject
{
    private int id;
    public string itemName;
    public Sprite sprite;
    public int maxDurability;
    public int maxAmount;
    public bool isTickable;
    public List<string> tags;


    public virtual void Use(InventoryController controller, Item item)
    {

    }
    public virtual bool CanUse(InventoryController controller, Item item)
    {
        return false;
    }
    public virtual void OnCreateItem(InventoryController controller, Item item)
    {

    }
    public virtual void OnDestroyItem(InventoryController controller, Item item)
    {

    }
    public virtual void Tick(InventoryController controller, Item item)
    {

    }
    public int GetId()
    {
        return id;
    }
}
