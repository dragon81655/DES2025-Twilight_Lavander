using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    // Start is called before the first frame update

    private InventoryController currentController;
    private List<CraftBase> availableRecipes;

    public void Init(InventoryController controller)
    {
        currentController = controller;
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
