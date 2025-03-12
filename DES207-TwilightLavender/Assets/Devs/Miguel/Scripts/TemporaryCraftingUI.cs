using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemporaryCraftingUI : MonoBehaviour, IAxisHandler, IInteractorHandler, IUsable1
{
    [SerializeField] private int currentlySelected;

    bool canMove;
    private CraftingController controller;
    private List<CraftBase> availableRecipes;

    [SerializeField] private GameObject craftingCanvas;
    [SerializeField] private TextMeshProUGUI ingridients;
    [SerializeField] private TextMeshProUGUI outputs;

    public void Interact()
    {
        //InputManager.instance.SwitchMiniGame(, gameObject);
        GameStateManager.instance.ContinueTimer();
        craftingCanvas.SetActive(false);
    }

    public void Move(float x, float y)
    {
        if (Mathf.Abs(y) > 0.1f && canMove)
        {
            currentlySelected = Mathf.Clamp((y > 0 ? 1 : -1) + currentlySelected, 0, availableRecipes.Count-1);
            UpdateInformation();
            canMove = false;
        }
        if(y == 0) canMove = true;
    }

    public void Use1()
    {
        controller.Craft(currentlySelected);
    }
    public void Init(List<CraftBase> availableRecipes, GameObject obj)
    {
        craftingCanvas.SetActive(true);
        GameStateManager.instance.StopTimer();
        //InputManager.instance.SwitchMiniGame(obj, gameObject);
        this.availableRecipes = availableRecipes;
        UpdateInformation();
    }
    private void UpdateInformation()
    {
        if(availableRecipes.Count == 0)
        {
            ingridients.text = "No recipes unlocked!";
            outputs.text = "No recipes unlocked!";
            return;
        }
        CraftBase recipe = availableRecipes[currentlySelected];
        ingridients.text = "";
        outputs.text = "";
        for (int i = 0; i < recipe.inputs.Count; i++)
        {
            ingridients.text += recipe.inputs[i].amount + " " + recipe.inputs[i].item.displayName + "\n";
        }
        for (int i = 0; i < recipe.outputs.Count; i++)
        {
            outputs.text += recipe.outputs[i].GetAmount() + " " + recipe.outputs[i].GetDisplayName();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CraftingController>();
    }
}
