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

    private Item item;

    public void Initialize(Item newItem) // create blank item in slot to then be updated with item info
    {
        item = newItem;
        UpdateSlot(item); // calling function to update item info
    }

    public void UpdateSlot(Item updatedItem) // updating the slot with relevant info (quantity, name, sprite)
    {
        itemQuantity.text = updatedItem.GetAmount().ToString(); // returning the item amount as a string
        itemName.text = updatedItem.GetDisplayName(); // returning the item name as a string
        itemSprite.sprite = updatedItem.GetItemSprite(); // getting item image
    }

    public void SetSelected(bool isSelected) // indicator to show if the slot is selected in the hotbar
    {
        if (isSelected) // if selected
        {
            slotBG.color = new Color(0.2f, 0.2f, 0.2f); // darken slot background
        }
        else
        {
            slotBG.color = new Color(0.5f, 0.5f, 0.5f); // else back to original colour
        }
    }
}
