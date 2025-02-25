using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class HotbarController : MonoBehaviour, IUsable1, IDropHandler
{
    private InventoryController ic;
    [SerializeField]
    private int currentSlot = 0;

    [SerializeField]
    private GameObject dropPrefab;
    void Start()
    {
        ic = GetComponent<InventoryController>();
    }
    private void UseItem()
    {
        Debug.Log("Try use item!");
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
            int mouse = (int)Input.mouseScrollDelta.y;
            if (mouse != 0)
            {
                currentSlot = Mathf.Clamp(currentSlot + mouse, 0, ic.GetSlotAmount()-1);
            }
            
        }
    }

    public void Use1()
    {
        UseItem();
    }

    public void Drop()
    {
        if (ic == null) return;
        Item i = ic.GetItem(currentSlot);
        if (i != null)
        {
            ic.RemoveItem(i);
            GameObject g = Instantiate(dropPrefab, transform.position + transform.forward, Quaternion.identity);
            bool t = g.GetComponent<InventoryController>().AddItem(i);
            Instantiate(i.GetDroppedModel(), g.transform).transform.localPosition = Vector3.zero;
            if (!t)
            {
                Debug.LogError("Something wrong with the droppedItemPrefab");
            }
        }
    }
}
