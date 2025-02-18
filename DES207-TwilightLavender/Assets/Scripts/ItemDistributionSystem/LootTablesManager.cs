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
        controllers= new Dictionary<string, List<LootTablesController>>();
        instance= this;
    }

    public void RegisterZone(LootTablesController controller)
    {
        if(!controllers.ContainsKey(controller.zoneId)) controllers.Add(controller.zoneId,new List<LootTablesController>());
        controllers[controller.zoneId].Add(controller);
    }

    private void Update()
    {
        if(firstTick)
        {
            foreach(MandatoryItem item in mandatoryItems)
            {
                for (int i = 0; i < item.amountInLevel; i++)
                {
                    string id = "";
                    if (item.zoneIds.Count != 0)
                    {
                        id = item.zoneIds[UnityEngine.Random.Range(0, item.zoneIds.Count)];
                        List<LootTablesController> t = controllers[id];
                        t[UnityEngine.Random.Range(0, t.Count)].SpawnItem(item.item);
                    }
                    else
                    {
                        List<string> keys = new List<string>(controllers.Keys);
                        List<LootTablesController> t = controllers[keys[UnityEngine.Random.Range(0, keys.Count)]];
                        int r = UnityEngine.Random.Range(0, t.Count);
                        t[r].SpawnItem(item.item);
                    }
                }
            }
            firstTick= false;
        }
    }

    private void OnValidate()
    {
        foreach(MandatoryItem i in mandatoryItems)
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
