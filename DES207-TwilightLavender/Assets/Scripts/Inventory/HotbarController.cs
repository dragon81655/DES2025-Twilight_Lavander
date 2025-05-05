using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HotbarController : MonoBehaviour, IUsable1, IDropHandler, IScrollable, ICamLockable, IInputChangeSummoner, IHiveMindSummoner
{
    private InventoryController ic;
    [SerializeField]
    private int currentSlot = 0;

    [SerializeField]
    private GameObject dropPrefab;
    
    private HotbarCTRL hotbarCTRL;

    public SFXManager SFXManager; // for sfx

    float timer = 0.1f;
    bool camLock = true;

    int selected = 0;

    private bool isHiveMind = false;

    bool isController = false;
    void Start()
    {
        ic = GetComponent<InventoryController>();
        hotbarCTRL = FindAnyObjectByType<HotbarCTRL>();
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
            timer-= Time.deltaTime;
            if(timer <= 0)
            {
                UpdateSlotUI();
                timer = 0.1f;
            }
        }
    }

    private void UpdateSlotUI()
    {
        if (ic == null) return;
        Item i = ic.GetItem(currentSlot);
        
        int amount = ic.GetCurrentItemAmount();
        for (int j = 0; j < amount; j++) 
        { 
            Item item = ic.GetItem(j);
            hotbarCTRL.UpdateSlot(item, j, j == currentSlot);
        }
        hotbarCTRL.CheckDestroy(amount);
    }

    public void Use1()
    {
        if (isHiveMind) return;
        UseItem();
    }

    public void Drop()
    {
        if (ic == null) return;
        Item i = ic.GetItem(currentSlot);
        if (i != null)
        {
            ic.RemoveItem(i);
            SFXManager.DropSFX(); // drop sfx
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

    public void CamLock()
    {
        camLock = !camLock;
    }

    public void Scroll(float val)
    {
        if (camLock || isController)
        {
            currentSlot = Mathf.Clamp((int)val + currentSlot, 0, ic.GetCurrentItemAmount() > 0 ? ic.GetCurrentItemAmount()-1 : 0);
        }
    }

    public void Notify()
    {
        isController = "KB" != InputManager.instance.GetInputType(gameObject);
        isHiveMind = false;
    }

    public void Summon()
    {
        isHiveMind = !isHiveMind;
    }
}
