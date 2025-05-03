using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingTableDestructor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private MiniGameWorldEvoker craftingTablePrefab;
    [SerializeField]
    private GameObject activeTableSignaliser;
    [SerializeField]
    private GameObject deadTableSignaliser;
    private bool passed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(passed) return;
        craftingTablePrefab.isActive = false;
        activeTableSignaliser.SetActive(false);
        deadTableSignaliser.SetActive(true);
        passed = true;
    }
}
