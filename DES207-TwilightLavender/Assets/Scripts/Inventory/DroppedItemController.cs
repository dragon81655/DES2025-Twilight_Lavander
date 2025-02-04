using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemController : MonoBehaviour, ITestInteractable
{
    [SerializeField]
    private List<ItemBase> items;
    public void Interact(GameObject source)
    {
        InventoryController ic = source.GetComponent<InventoryController>();
        if (ic)
        {
                
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
