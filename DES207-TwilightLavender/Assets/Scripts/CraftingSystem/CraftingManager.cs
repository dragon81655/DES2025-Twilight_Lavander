using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] private List<CraftBase> craftRegistry;
    public static CraftingManager instance;
    void Awake()
    {
        instance= this;
        CleanRegistry();
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
