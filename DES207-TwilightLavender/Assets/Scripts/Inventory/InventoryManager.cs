using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InventoryManager instance;
    [SerializeField] private List<ItemBase> itemRegistry;
    void Awake()
    {
        instance= this;
    }

    public Item CreateItem(int id, int amount = -1, int durability = -1, List<string> tags = null, string displayName = "")
    {
        if(itemRegistry.Count <= id) return null;

        if (itemRegistry[id] != null)
        {
            Item t = new Item(itemRegistry[id]);
            if(amount != -1) t.AddAmount(amount);
            if(durability != -1) t.AddDurability(durability);
            if(tags!= null) t.tags.AddRange(tags);
            if(displayName != "") t.SetDisplayName(displayName);
            return t;
        }

        return null;
    }
    public Item CreateItem(string itemName, int amount = -1, int durability = -1, List<string> tags = null, string displayName = "")
    {
        for(int i = 0; i < itemRegistry.Count; i++)
        {
            if (itemRegistry[i].itemName == itemName)
            {
                return CreateItem(i, amount, durability, tags, displayName);
            }
        }
        return null;
    }

    public Item GetRandomItem()
    {
        return new Item(itemRegistry[Random.Range(0, itemRegistry.Count)]);
    }
}
