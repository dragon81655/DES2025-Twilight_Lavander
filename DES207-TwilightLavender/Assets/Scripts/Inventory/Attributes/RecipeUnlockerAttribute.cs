using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUnlockerAttribute : AttributeBase
{

    [SerializeField] private List<CraftBase> recipes;
    public override AttributeBase Copy()
    {
        RecipeUnlockerAttribute toReturn = new RecipeUnlockerAttribute();
        toReturn.recipes = this.recipes;
        toReturn.attributeName= this.attributeName;
        return toReturn;
    }

    public override void Init(InventoryController controller, Item item)
    {

    }

    public override void RunAttribute(InventoryController controller, Item item)
    {
        foreach(CraftBase craft in recipes)
        {
            Debug.Log("Unlocked " + craft.craftId);
            craft.unlocked= true;
        }
    }

    public override void TickAttribute(InventoryController controller, Item item)
    {

    }
}
