using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HiveMindInputController : MonoBehaviour, IAxisHandler, IHiveMindSummoner, IInputChangeSummoner, IUsable1
{
    private HiveMindController hiveMindController;
    private HiveMindFinalUIController hiveMindFinalUIController;

    [SerializeField] private HiveMindFinalUIController humanUI;
    [SerializeField] private HiveMindFinalUIController virusUI;
    private bool canMove = false;
    private bool selectingHM = false;
    private int currentlySelected = 0;

    [SerializeField] private bool isBody;
    public void Move(float x, float y)
    {
        if(!selectingHM) return;
        if (Mathf.Abs(y) > 0.1f && canMove)
        {
            currentlySelected = Mathf.Clamp((y > 0 ? 1 : -1) + currentlySelected, 0, hiveMindController.GetAbilityCount() - 1);
            UpdateInformation();
            canMove = false;
        }
        if (y == 0) canMove = true;
    }

    public void Summon()
    {
        selectingHM = !selectingHM;
    }

    private void UpdateInformation()
    {
        HiveMindAbility ability = hiveMindController.GetAbility(currentlySelected);
        Debug.Log("AB Name: " + ability.abilityName + "\nAB Desc:" + ability.abilityDesc);
        hiveMindFinalUIController.UpdateSlots(currentlySelected - 1 < 0 ? -1 : hiveMindController.GetAbility(currentlySelected-1).spriteId, hiveMindController.GetAbility(currentlySelected).spriteId, currentlySelected + 1 >= hiveMindController.GetAbilityCount() ? -1 : hiveMindController.GetAbility(currentlySelected+1).spriteId);
    }

    // Start is called before the first frame update
    void Start()
    {
        hiveMindController = GetComponent<HiveMindController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Notify()
    {
        selectingHM = false;
        if (InputManager.instance.isVirusOnBody() == isBody)
        {
            hiveMindFinalUIController = virusUI;
            UpdateInformation();
        }
        else
        {
            hiveMindFinalUIController = humanUI;
            UpdateInformation();
        }
    }

    public void Use1()
    {
        if (selectingHM)
        {
            hiveMindController.GetAbility(currentlySelected).Act(hiveMindController);
        }
    }
}
