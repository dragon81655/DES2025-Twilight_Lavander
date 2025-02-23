using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingController : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update

    private InventoryController currentController;
    private List<CraftBase> availableRecipes;
    public void Interact(GameObject source)
    {
        //Open UI window and add this as the current controller.
        Debug.Log("Interacted");
        CraftingManager.instance.CheckUnlockedCrafts();
        availableRecipes = (List<CraftBase>)CraftingManager.instance.GetUnlockedCrafts();
        currentController = source.GetComponent<InventoryController>();
        if(currentController != null )
        {
            TestUIManager.instance.craftUI.OpenMenu(this);
        }
        
    }

    public IEnumerable<CraftBase> GetAvailableRecipes()
    {
        availableRecipes = (List<CraftBase>)CraftingManager.instance.GetUnlockedCrafts();
        return availableRecipes;
    }

    public void Craft(int index)
    {
        availableRecipes[index].TryCraft(currentController);
    }
}
