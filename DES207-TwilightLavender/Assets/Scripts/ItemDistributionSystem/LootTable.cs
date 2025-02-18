using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private int minAmountOfItemsToSpawn;
    [SerializeField]
    private int maxAmountOfItemsToSpawn;

    [SerializeField]
    private List<LootTableItem> items;

    [SerializeField, HideInInspector]
    private float chances = 0;
    private void OnValidate()
    {
        chances = 0;
        foreach (LootTableItem item in items)
        {
            chances += item.spawnChance;
        }
    }
    public IEnumerable<Item> CreateItemsFromLootTable()
    {
        List<Item> toReturn = new List<Item>();
        int amountToSpawn = UnityEngine.Random.Range(minAmountOfItemsToSpawn, maxAmountOfItemsToSpawn);
        for (int i = 0; i < amountToSpawn; i++)
        {
            float spawnItem = UnityEngine.Random.Range(0, chances);
            float cChance = chances;
            foreach (LootTableItem item in items)
            {
                cChance -= item.spawnChance;
                if (cChance <= spawnItem)
                {
                    Item t = new Item(item.itemBase);
                    t.AddAmount(UnityEngine.Random.Range(item.minAmount, item.maxAmount));
                    toReturn.Add(t);
                    break;
                }
            }
        }
        return toReturn;
    }
}

[Serializable]
public struct LootTableItem
{
    [Header("Specific item")]

    public ItemBase itemBase;
    public int minAmount;
    public int maxAmount;

    [Header("Spawn preferences")]
    [Range(0f, 1f)]public float spawnChance;
}