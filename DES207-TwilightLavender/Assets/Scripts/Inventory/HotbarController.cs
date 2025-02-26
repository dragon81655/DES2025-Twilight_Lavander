using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarController : MonoBehaviour, IUsable1, IDropHandler
{
    private InventoryController ic;
    [SerializeField]
    private int currentSlot = 0;

    [SerializeField]
    private GameObject dropPrefab;

    [SerializeField]
    private TextMeshProUGUI slotInfo;

    float timer = 1;
    void Start()
    {
        ic = GetComponent<InventoryController>();
        UpdateSlotUI();
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
                UpdateSlotUI();
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
                currentSlot = Mathf.Clamp(currentSlot + mouse, 0, ic.GetSlotAmount() - 1);
                UpdateSlotUI();
            }

            timer-= Time.deltaTime;
            if(timer <= 0)
            {
                UpdateSlotUI();
                timer = 1;
            }
        }
    }

    private void UpdateSlotUI()
    {
        if (ic == null) return;
        Item i = ic.GetItem(currentSlot);
        if (i != null)
        {
            slotInfo.text = "Slot number: " + (currentSlot + 1);
            slotInfo.text += "\nItem name: " + i.GetDisplayName();
            slotInfo.text += "\nAmount: " + i.GetAmount() + "/" + i.GetMaxAmount();
        }
        else
        {
            slotInfo.text = "Slot number: " + (currentSlot + 1);
            slotInfo.text += "\nNo Item ";
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
            UpdateSlotUI();
            if (!t)
            {
                Debug.LogError("Something wrong with the droppedItemPrefab");
            }
        }
    }
}
