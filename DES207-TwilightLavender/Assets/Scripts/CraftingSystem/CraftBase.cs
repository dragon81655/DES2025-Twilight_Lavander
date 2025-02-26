using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Crafts/CraftBase")]
public class CraftBase : ScriptableObject
{
    public string craftId;
    public string displayName;
    public Sprite recipeSprite;

    [SerializeReference]
    public List<CraftItem> inputs;
    public List<Item> outputs;
    public bool startUnlocked;
    //[HideInInspector]
    public bool unlocked;


    public List<string> tags;

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
            foreach (CraftItem c in inputs)
                controller.TakeItemAmount(c.item.id, c.amount);
            List<Item> toReturn = new List<Item>();
            foreach (Item item in outputs)
            {
                toReturn.Add(new Item(item));
            }
            return controller.AddItemRange(toReturn);

        }
        return null;
    }

    public void Init()
    {
        unlocked = startUnlocked;
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

