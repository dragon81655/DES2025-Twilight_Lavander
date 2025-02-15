using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    private InventoryController ic;
    [SerializeField]
    private int currentSlot = 0;
    void Start()
    {
        ic = GetComponent<InventoryController>();
    }
    private void UseItem()
    {
        if (ic == null) return;
        Item i = ic.GetItem(currentSlot);
        if (i != null)
        {
            if (i.CanUse())
            {
                i.Use();
            }
        }
    }
    private void Update()
    {
        if(ic != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UseItem();
            }
            int mouse = (int)Input.mouseScrollDelta.y;
            if (mouse != 0)
            {
                currentSlot = Mathf.Clamp(currentSlot + mouse, 0, ic.GetSlotAmount()-1);
            }
            
        }
    }
}
