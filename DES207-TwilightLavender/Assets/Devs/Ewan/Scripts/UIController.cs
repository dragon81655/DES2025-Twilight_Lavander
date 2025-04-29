using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using JetBrains.Annotations;
using static UnityEditor.Progress;

public class UIController : MonoBehaviour
{
    // UI Elements

    public Image HumanAbilityMenu; // grabbing human ability menu
    public Image VirusAbilityMenu; // grabbing virus ability menu
    public TextMeshProUGUI TimerText; // grabbing timer text
    public GameObject DialogueContainer; // grabbing dialogue box
    public GameObject uiContainer; // grabbing UI
    public TextMeshProUGUI VirusWinsText; // grabbing virus wins text
    public TextMeshProUGUI HumanWinsText; // grabbing human wins text
    public TextMeshProUGUI InteractText; // grabbing interact text

    // Takeover Timer Settings

    private float switchTimerB; // getting time limit for bar
    private float switchTimerValueB; // getting time remaining for bar

    // Human Ability Menu Settings

    public GameObject[] HumanAbilIcons; // grabbing human ability icons
    public Transform[] HumanAbilSlots; // for moving slot icons

    // Virus Ability Menu Settings

    public GameObject[] VirusAbilIcons; // grabbing virus ability icons
    public Transform[] VirusAbilSlots; // for moving slot icons

    // Ability Timer Settings



    // Misc

    public bool InteractRange; // whether or not player is within range to interact
    public bool MenuOpen; // check whether dialogue menu is open

    // Other Scripts

    ProxInteraction ProxInteractionScript; // calling prox interaction script
    InventoryController invController; // calling inventory controller script
    public GameStateManager GameStateManager; // grabbing game state manager script
    public HiveMindController HiveMindController; // grabbing hive mind controller script

    // Start is called before the first frame update
    void Start()

    {

        VirusWinsText.gameObject.SetActive(false); // ensuring virus wins is disabled from start
        HumanWinsText.gameObject.SetActive(false); // ensuring human wins is disabled from start

        ProxInteractionScript = GameObject.FindGameObjectWithTag("uiTag").GetComponent<ProxInteraction>(); // calling interaction script
        invController = GetComponent<InventoryController>(); // calling inventory script
    }

    // Update is called once per frame
    void Update()

    {
        // Human Ability Code


        if (Input.GetKeyDown(KeyCode.X)) // checking for input to cycle human ability icon
        {
            HCycleIcons(); // calling human ability cycle function
        }

        if (Input.GetKeyDown(KeyCode.F)) // checking for input to use human ability
        {
            HAbilityUse(); // calling human ability use
        }


        // Virus Ability Code


        if (Input.GetKeyDown(KeyCode.O)) // checking for input to cycle virus ability icon
        {
            VCycleIcons(); // calling virus ability cycle function
        }

        if (Input.GetKeyDown(KeyCode.L)) // checking for input to use virus ability
        {
            VAbilityUse(); // calling virus ability use
        }
    }

    private void FixedUpdate()

    {
        if (InteractRange == false) // hiding dialogue and interact elements when not in range
        {
            DialogueContainer.gameObject.SetActive(false);
            InteractText.gameObject.SetActive(false);
            MenuOpen = false;
        }
    }

    void VirusWins() // function for Virus win state

    {
        uiContainer.SetActive(false); // disable UI
        VirusWinsText.gameObject.SetActive(true); // enable virus win screen
    }

    public void HumanWins() // function for human win state
    {
        uiContainer.SetActive(false); // disable UI
        HumanWinsText.gameObject.SetActive(true); // enable human win screen
    }

    public void Interact() // interaction function

    {
        InteractRange = true;
        if (Input.GetKeyDown(KeyCode.E) && InteractRange == true) // condition for opening dialogue box
        {
            DialogueContainer.SetActive(true); // open dialogue box
            MenuOpen = true; // set menu to open
        }

        if (Input.GetKeyDown(KeyCode.R) && MenuOpen == true) // condition for closing dialogue box willingly
        {
            DialogueContainer.SetActive(false); // close dialogue box
            MenuOpen = false; // set menu to closed
        }
    }


    // Human Ability Menu Code


    void HCycleIcons() // function for cycling human ability icons
    {
        HRotateIconsArray(); // rotating array

        for (int i = 0; i < HumanAbilIcons.Length; i++) // moving icons
        {
            HumanAbilIcons[i].transform.position = HumanAbilSlots[i].position;
        }

        HMiddleSlot(); // calling human middle slot identifier
    }

    void HRotateIconsArray() // function for assigning correct icon positions
    {
        GameObject firstIcon = HumanAbilIcons[0];
        for (int i = 0; i < HumanAbilIcons.Length - 1; i++) // how many slots to move
        {
            HumanAbilIcons[i] = HumanAbilIcons[i + 1];
        }
        HumanAbilIcons[HumanAbilIcons.Length - 1] = firstIcon;
    }

    void HMiddleSlot() // function for identifying what icon is in middle slot, mainly for testing purposes
    {
        GameObject middleSlotIcon = HumanAbilIcons[1]; // slot 2 is index 1 in the array
        Debug.Log("Middle Slot: " + middleSlotIcon.name); // printing slot icon for testing
    }

    void HAbilityUse() // function for using human ability
    {
        GameObject middleSlotIcon = HumanAbilIcons[1]; // slot 2 is index 1 in the array

        if (middleSlotIcon.name == "HSkill") // if skill check is in middle slot
        {
            Debug.Log("Call Skill Check");
        }

        else if (middleSlotIcon.name == "HDecontam") // if decontamination is in middle slot
        {
            Debug.Log("Call Decontamination");
        }

        else if (middleSlotIcon.name == "HDoor") // if door is in middle slot
        {
            Debug.Log("Call Door");
        }
        else
        {
            Debug.Log("No Match"); // for bug testing
        }
    }


    // Virus Ability Menu Code


    void VCycleIcons() // function for cycling virus ability icons
    {
        VRotateIconsArray(); // rotating array

        for (int i = 0; i < VirusAbilIcons.Length; i++) // moving icons
        {
            VirusAbilIcons[i].transform.position = VirusAbilSlots[i].position;
        }

        VMiddleSlot(); // calling virus middle slot identifier
    }

    void VRotateIconsArray() // function for assigning correct icon positions
    {
        GameObject firstIcon = VirusAbilIcons[0];
        for (int i = 0; i < VirusAbilIcons.Length - 1; i++) // how many slots to move
        {
            VirusAbilIcons[i] = VirusAbilIcons[i + 1];
        }
        VirusAbilIcons[VirusAbilIcons.Length - 1] = firstIcon;
    }

    void VMiddleSlot() // function for identifying what icon is in middle slot, mainly for testing purposes
    {
        GameObject middleSlotIcon = VirusAbilIcons[1]; // slot 2 is index 1 in the array
        Debug.Log("Middle Slot: " + middleSlotIcon.name); // printing slot icon for testing
    }

    void VAbilityUse() // function for using virus ability
    {
        GameObject middleSlotIcon = VirusAbilIcons[1]; // slot 2 is index 1 in the array

        if (middleSlotIcon.name == "VSkill") // if skill check is in middle slot
        {
            HiveMindAbility ability = HiveMindController.GetAbility(0); 
            if (ability is CastMinigameAbility castMinigameAbility)
            {
                castMinigameAbility.Act(HiveMindController);
            }
            else
            {
                Debug.Log("The ability at this index is not a CastMinigameAbility");
            }
                Debug.Log("Call Skill Check");
        }



        else if (middleSlotIcon.name == "VContam") // if contamination/spore cloud is in middle slot
        {
            HiveMindAbility ability = HiveMindController.GetAbility(1);
            if (ability is ContaminationZoneAbility contaminationZoneAbility)
            {
                contaminationZoneAbility.Act(HiveMindController);
            }
            else
            {
                Debug.Log("The ability at this index is not a ContaminationZoneAbility");
            }
                Debug.Log("Call Contamination/Spore Cloud");
        }



        else if (middleSlotIcon.name == "VDoor") // if door is in middle slot
        {
            HiveMindAbility ability = HiveMindController.GetAbility(2);
            if (ability is DoorControllerAbility doorControllerAbility)
            {
                doorControllerAbility.Act(HiveMindController);
            }
            else
            {
                Debug.Log("The ability at this index is not a DoorControllerAbility");
            }

                Debug.Log("Call Door");
        }


        else
        {
            Debug.Log("No Match"); // for bug testing
        }
    }
}
