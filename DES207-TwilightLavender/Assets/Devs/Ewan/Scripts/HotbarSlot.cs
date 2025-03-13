using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    public Text itemQuantity; // grabbing item quantity
    public Text itemName; // grabbing item name

    private Item item;

    public void Initialize(Item newItem) // create blank item in slot to then be updated with item info
    {
        item = newItem;
        UpdateSlot(item); // calling function to update item info
    }

    public void UpdateSlot(Item updatedItem) // updating the slot with relevant info (quantity, name, sprite)
    {
        itemQuantity.text = updatedItem.GetAmount().ToString(); // returning the item amount as a string
        itemName.text = updatedItem.GetItemName().ToString(); // returning the item name as a string
    }
}
