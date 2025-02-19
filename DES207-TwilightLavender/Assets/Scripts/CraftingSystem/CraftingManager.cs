using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private List<CraftBase> craftRegistry;
    [SerializeField] protected List<CraftBase> unlockedCrafts;
    public static CraftingManager instance;

    void Awake()
    {
        instance= this;
        CleanRegistry();
        CheckUnlockedCrafts();
    }
    public void CheckUnlockedCrafts()
    {
        foreach (CraftBase c in craftRegistry)
        {
            if (c != null)
            {
                if (c.unlocked)
                {
                    unlockedCrafts.Add(c);
                }
            }
        }
    }
    public IEnumerable<CraftBase> GetUnlockedCrafts()
    {
        return unlockedCrafts;
    }
    private void CleanRegistry()
    {
        if (craftRegistry != null)
        {
            for (int i = 0; i < craftRegistry.Count; i++)
            {
                if (craftRegistry[i] == null) craftRegistry.RemoveAt(i);
            }
        }
    }
}
