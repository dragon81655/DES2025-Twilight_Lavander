using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    InventoryController inventoryController; // getting inventory script
    UIController uiController; // getting ui controller script

    public GameObject Player; // grabbing player
    public GameObject HotbarHolder; // grabbing hotbar
    public GameObject[] HotbarSlots; // hotbar array

    public int SlotTotal = 0; // total inventory slots

    public Image[] hotbarSlots = new Image[9];  // for hotbar images
    public Sprite[] slotImages;  // above

    // Start is called before the first frame update
    void Start()
    {
        inventoryController = GetComponent<InventoryController>(); // grabbing inv controller
        uiController = GetComponent<UIController>(); // grabbing UI controller
        HotbarHolder.SetActive(false); // disabling hotbar since you start with nothing

        // Make sure the hotbarSlots array is assigned via the Inspector
        if (hotbarSlots.Length == 0)
        {
            Debug.LogError("Hotbar slots not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        SlotManager();
    }

    public void SlotManager()
    {
        if (SlotTotal == 0)
        {
            foreach (GameObject slot in HotbarSlots)
            {
                slot.SetActive(false);
            }
            HotbarHolder.SetActive(false);  // disable hotbar if 0 slots
            return;
        }

        else
        {
            HotbarHolder.SetActive(true); // enable if more than 0 slots
            UpdateSlots(); // call function
        }
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (i < SlotTotal)
            {
                hotbarSlots[i].gameObject.SetActive(true);  // enable slot
                if (i < slotImages.Length)
                {
                    hotbarSlots[i].sprite = slotImages[i];  // give image
                }
            }
            else
            {
                hotbarSlots[i].gameObject.SetActive(false);  // cap display at 9 slots
            }
            if (SlotTotal > 9) // if more than 9 slots, reset to 9
            {
                SlotTotal = 9;
            }
            else if (SlotTotal < 0) // if less than 0 slots, reset to 0
            {
                SlotTotal = 0;
            }
        }
    }
}