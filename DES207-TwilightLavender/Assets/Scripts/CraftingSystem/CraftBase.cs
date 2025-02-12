using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Crafts/CraftBase")]
public class CraftBase : ScriptableObject
{
    [SerializeField]
    public string craftId;
    [SerializeReference]
    public List<CraftItem> inputs;
    [SerializeField]
    public List<Item> outputs;
    [SerializeField]
    public bool unlocked;

    public bool IsCraftable(InventoryController controller)
    {
        if(!unlocked) return false;
        foreach(CraftItem c in inputs)
        {
            if(!CheckIngredient(c, controller)) return false;
        }
        return true;
    }

    private bool CheckIngredient(CraftItem item, InventoryController controller)
    {
        if(item.item != null)
        {
            return controller.HasItem(item.item, item.amount);
        }
        else
        {
            return controller.HasItemByTag(item.tag, item.amount);
        }
    }

    //If returns null, it can't be crafted!
    public IEnumerable<Item> TryCraft(InventoryController controller)
    {
        if(!unlocked) return null;
        if (IsCraftable(controller))
        {
            return controller.AddItemRange(outputs);            
        }
        return null;
    }
}

[Serializable]
public class CraftItem
{
    public string tag;
    public ItemBase item;
    public int amount;

}

public struct OutputItem
{
    public ItemBase item;
    public int amount;
}

