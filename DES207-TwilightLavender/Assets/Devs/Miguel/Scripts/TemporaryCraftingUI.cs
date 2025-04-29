using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class TemporaryCraftingUI : BaseActivityController, IAxisHandler, IInteractorHandler, IUsable1
{
    [SerializeField] private int currentlySelected;

    bool canMove;
    private CraftingController controller;
    private List<CraftBase> availableRecipes;

    [SerializeField] private TextMeshProUGUI ingridients;
    [SerializeField] private TextMeshProUGUI outputs;

    private MiniGameController source;

    public void Interact()
    {
        OnFinish();
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

    public override GameObject GetControllable(MiniGameController source)
    {
        return gameObject;
    }

    public override void Init(MiniGameController source, IMiniGameDependent objs)
    {
        CraftingManager.instance.CheckUnlockedCrafts();
        this.source= source;
        this.controller = GetComponent<CraftingController>();
        this.controller.Init(source.GetComponent<InventoryController>());
        this.availableRecipes = (List<CraftBase>)controller.GetAvailableRecipes();
        UpdateInformation();
    }

    public override void Resume(int result)
    {
    }

    public override void Pause()
    {
    }

    public override void OnFinish()
    {
        source.OnFinishMiniGame(0);
        Destroy(gameObject);
    }
}
