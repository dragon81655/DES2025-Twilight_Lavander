using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiveMindUIController : BaseActivityController, IInteractorHandler, IAxisHandler, IUsable1
{
    [SerializeField] private int currentlySelected;
    private MiniGameController source;
    bool canMove;
    private HiveMindController controller;

    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private TextMeshProUGUI feedBack;

    public void Interact()
    {
        OnFinish();
    }

    public void Move(float x, float y)
    {
        if (Mathf.Abs(y) > 0.1f && canMove)
        {
            currentlySelected = Mathf.Clamp((y > 0 ? 1 : -1) + currentlySelected, 0, controller.GetAbilityCount() - 1);
            UpdateInformation();
            canMove = false;
        }
        if (y == 0) canMove = true;
    }

    public void Use1()
    {
        controller.UseAbility(currentlySelected);
    }
    private void UpdateInformation()
    {
        if (currentlySelected < 0) return;
        HiveMindAbility ha = controller.GetAbility(currentlySelected);
        if (ha != null)
        {
            abilityName.text = ha.abilityName;
            desc.text = ha.abilityDesc;
            feedBack.text = "";
        }
        else
        {
            Debug.LogError("Null Ability! "+ currentlySelected);
        }
    }

    public override GameObject GetControllable(MiniGameController source)
    {
        return gameObject;
    }

    public override void Init(MiniGameController source)
    {
        InputManager.instance.LockSwitch();
        this.source = source;
        controller = source.GetComponent<HiveMindController>();
        currentlySelected = 0;
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
        InputManager.instance.UnlockSwitch();
        Destroy(gameObject);
    }
}
