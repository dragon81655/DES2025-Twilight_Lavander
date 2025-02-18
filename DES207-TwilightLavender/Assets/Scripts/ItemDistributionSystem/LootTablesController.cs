using System.Collections.Generic;
using UnityEngine;

public class LootTablesController : MonoBehaviour
{
    private const string default_name = "default_zone";
    [SerializeField]
    private LootTable lootTable;

    public string zoneId;

    public Vector3 spawnArea;
    [SerializeField] private GameObject droppedItemPrefab;
    void Start()
    {
        if (LootTablesManager.instance != null) LootTablesManager.instance.RegisterZone(this);
        else Debug.LogError("No LootTablesManager in scene");
        if (zoneId == "" || zoneId == null)
        {
            zoneId = default_name;
            Debug.LogWarning("Item spawn zone " + gameObject.name + " does not have a zone id. Id defined to " + default_name);
        }
        SpawnLootTable();
    }

    

    public void SpawnItem(Item item)
    {
        float x = spawnArea.x / 2;
        float y = spawnArea.y / 2;
        float z = spawnArea.z / 2;
        while (true)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-x, x), Random.Range(-y, y), Random.Range(-z, z));
            if (!Physics.CheckBox(pos, droppedItemPrefab.transform.localScale / 2))
            {
                GameObject g = Instantiate(droppedItemPrefab, pos, Quaternion.identity);
                bool t = g.GetComponent<InventoryController>().AddItem(item);
                if (!t)
                {
                    Debug.LogError("Something wrong with the droppedItemPrefab");
                }
                else break;
            }
        }
    }

    private void SpawnLootTable()
    {
        if (lootTable == null) return;
        List<Item> items = (List<Item>)lootTable.CreateItemsFromLootTable();
        float x = spawnArea.x / 2;
        float y = spawnArea.y / 2;
        float z = spawnArea.z / 2;
        foreach (Item item in items)
        {
            SpawnItem(item);
        }
    }
}
