using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Text itemQuantity; // grabbing item quantity
    public Text itemName; // grabbing item name
    public Image itemSprite; // grabbing slot image
    public Image slotBG; // grabbing slot background

    public Item item;

    public void Initialize(Item newItem) // create blank item in slot to then be updated with item info
    {
        item = newItem;
        //UpdateSlot(item); // calling function to update item info
    }

    public void UpdateSlot() // updating the slot with relevant info (quantity, name, sprite)
    {
        itemQuantity.text = item.GetAmount().ToString(); // returning the item amount as a string
        itemName.text = item.GetDisplayName(); // returning the item name as a string
        itemSprite.sprite = item.GetItemSprite(); // getting item image
    }

    public void SetSelected(bool isSelected) // indicator to show if the slot is selected in the hotbar
    {
        if (isSelected) // if selected
        {
            Debug.Log("Selected!");
            slotBG.color = new Color(0.2f, 0.2f, 0.2f); // darken slot background
        }
        else
        {
            slotBG.color = new Color(0.5f, 0.5f, 0.5f); // else back to original colour
        }
    }
}
