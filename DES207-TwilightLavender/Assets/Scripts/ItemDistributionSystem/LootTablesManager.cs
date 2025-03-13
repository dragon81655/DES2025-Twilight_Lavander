using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTablesManager : MonoBehaviour
{
    public static LootTablesManager instance;
    private Dictionary<string, List<LootTablesController>> controllers;

    [SerializeField] private List<MandatoryItem> mandatoryItems;

    bool firstTick = true;
    void Awake()
    {
        controllers = new Dictionary<string, List<LootTablesController>>();
        instance = this;
    }

    public void RegisterZone(LootTablesController controller)
    {
        if (!controllers.ContainsKey(controller.zoneId)) controllers.Add(controller.zoneId, new List<LootTablesController>());
        controllers[controller.zoneId].Add(controller);
    }

    private void Update()
    {
        if (firstTick)
        {
            if (mandatoryItems == null) return;
            foreach (MandatoryItem item in mandatoryItems)
            {
                for (int i = 0; i < item.amountInLevel; i++)
                {
                    string id = "";
                    if (item.zoneIds.Count != 0)
                    {
                        id = item.zoneIds[UnityEngine.Random.Range(0, item.zoneIds.Count)];
                        if (!SpawnItem(item.item, id))
                        {
                            Debug.LogWarning("An non existent zone was selected!\n" + item.item.GetItemName());
                            bool spawned = false;
                            foreach (string s in item.zoneIds)
                            {
                                if (SpawnItem(item.item, s))
                                {
                                    spawned = true;
                                    break;
                                }
                            }
                            if (spawned) continue;
                            if (!SpawnAnywhere(item.item))
                            {
                                Debug.LogError("No area to spawn items");
                                firstTick = false;
                                return;
                            }
                        }

                    }
                    else
                    {
                        if (!SpawnAnywhere(item.item))
                        {
                            Debug.LogError("No area to spawn items");
                            firstTick = false;
                            return;
                        }

                    }
                }
            }
            firstTick = false;
        }
    }

    private bool SpawnItem(Item item, string zoneId)
    {
        if (controllers.ContainsKey(zoneId))
        {
            List<LootTablesController> t = controllers[zoneId];
            t[UnityEngine.Random.Range(0, t.Count)].SpawnItem(item);
            return true;
        }
        return false;
    }
    private bool SpawnAnywhere(Item item)
    {
        List<string> keys = new List<string>(controllers.Keys);
        if (keys.Count == 0) return false;
        List<LootTablesController> t = controllers[keys[UnityEngine.Random.Range(0, keys.Count)]];
        int r = UnityEngine.Random.Range(0, t.Count);
        t[r].SpawnItem(item);
        return true;
    }
    private void OnValidate()
    {
        foreach (MandatoryItem i in mandatoryItems)
        {
            i.item.OnGUI();
        }
    }
}

[Serializable]
public struct MandatoryItem
{
    public Item item;
    public List<string> zoneIds;
    public int amountInLevel;
}