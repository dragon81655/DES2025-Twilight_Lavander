using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSelector : MonoBehaviour
{
    public HotbarCTRL hotbarCTRL; // grabbing hotbar controller script
    public int selectedIndex = 0; // tracking selected slot

    void Update()
    {
        //HotbarSelect(); // function for selecting hotbar slot
    }

    void HotbarSelect() // function for selecting hotbar slot
    {
        int previousIndex = selectedIndex;

        for (int i = 0; i < Mathf.Min(6, hotbarCTRL.hotbarSlots.Count); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // getting number inputs for selection
            {
                selectedIndex = i;
            }
        }

        for (int i = 0; i < hotbarCTRL.hotbarSlots.Count; i++)
        {
            HotbarSlot slot = hotbarCTRL.hotbarSlots[i].GetComponent<HotbarSlot>();
            if (slot != null)
            {
                slot.SetSelected(i == selectedIndex); // darken the selected slot
            }
        }

        if (previousIndex != selectedIndex)
        {
            Debug.Log("Selected slot: " + (selectedIndex + 1)); // debug method for testing
        }
    }
}
