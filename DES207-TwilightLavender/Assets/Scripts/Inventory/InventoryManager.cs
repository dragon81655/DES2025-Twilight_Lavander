using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Callbacks;
using Unity.VisualScripting;
using System.Threading;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InventoryManager instance;

    [SerializeField] private List<ItemBase> itemRegistry;
    void Awake()
    {
        instance= this;
        CleanRegistry();
    }
    //When any scriptableObject is deleted, this is called
    private void CleanRegistry()
    {
        for (int i = 0; i < itemRegistry.Count; i++)
        {
            if (itemRegistry[i] == null || itemRegistry[i].attributes == null) 
                itemRegistry.RemoveAt(i);
            else
            {
                itemRegistry[i].id = i;
            }
        }
    }
    //Called through reflection on the InventoryWindow Editor Script
    private void AddItemToRegistry(ItemBase item)
    {
        if(itemRegistry != null)
        {
            itemRegistry.Add(item);
            Debug.Log("Item " + item.itemName + " added!");
            CleanRegistry();
        }
    }
    private Item CreateItem(int id, int amount = -1, int durability = -1, List<string> tags = null, string displayName = "")
    {
        if(itemRegistry.Count <= id) return null;

        if (itemRegistry[id] != null)
        {
            Item t = new Item(itemRegistry[id]);
            if(amount != -1) t.AddAmount(amount);
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

    //DO NOT USE ON UPDATES
    public Item GetRandomItemWithTag(IEnumerable<string> tags, bool mustContainAll)
    {
        List<ItemBase> possibilities = new List<ItemBase>();
        foreach (ItemBase i in itemRegistry)
        {
            bool hasAllTags = true;
            foreach (string tag in tags)
            {
                if (i.tags.Contains(tag))
                {
                    if (!mustContainAll)
                    {
                        possibilities.Add(i);
                        break;
                    }
                }
                else
                {
                    if (mustContainAll)
                    {
                        hasAllTags = false;
                        break;
                    }
                }
            }
            if (mustContainAll && hasAllTags)
            {
                possibilities.Add(i);
            }
        }
        return possibilities.Count == 0 ? null : new Item(possibilities[Random.Range(0, possibilities.Count)]);
    }
}
