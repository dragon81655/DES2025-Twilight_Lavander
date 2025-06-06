using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    // UI Elements

    public Image HumanAbilityMenu; // grabbing human ability menu
    public Image VirusAbilityMenu; // grabbing virus ability menu

    public GameObject uiContainer; // grabbing UI
    public GameObject PauseMenu; // grabbing pause menu

    public GameObject AbilDecontam; // for human abil menu
    public GameObject AbilHDoor; // for human abil menu
    public GameObject AbilHSkill; // for human abil menu

    public GameObject AbilContam; // for virus abil menu
    public GameObject AbilVDoor; // for virus abil menu
    public GameObject AbilVSkill; // for virus abil menu

    public TextMeshProUGUI TimerText; // grabbing timer text

    // Human Ability Menu Settings

    public GameObject[] HumanAbilIcons; // grabbing human ability icons
    public Transform[] HumanAbilSlots; // for moving slot icons

    // Virus Ability Menu Settings

    public GameObject[] VirusAbilIcons; // grabbing virus ability icons
    public Transform[] VirusAbilSlots; // for moving slot icons

    // Misc

    public bool InteractRange; // whether or not player is within range to interact
    public bool MenuOpen; // check whether dialogue menu is open

    // Other Scripts

    InventoryController invController; // calling inventory controller script
    public GameStateManager GameStateManager; // grabbing game state manager script
    public HiveMindController HiveMindController; // grabbing hive mind controller script
    public ContaminationZoneController ContaminationZoneController; // grabbing zone script for abil
    public DoorController DoorController; // grabbing door control for abil
    public PauseMenu PauseGame; // grabbing pause menu

    // Start is called before the first frame update
    void Start()

    {

        PauseMenu.SetActive(false); // making sure pause menu is disabled on start

        invController = GetComponent<InventoryController>(); // calling inventory script
    }

    // Update is called once per frame
    void Update()

    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }

        // Human Ability Code


        /*if (Input.GetKeyDown(KeyCode.X)) // checking for input to cycle human ability icon
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
        }*/
    }

    private void FixedUpdate()

    {

    }

    public void HideAll() // for hiding all
    {
        uiContainer.gameObject.SetActive(false);
    }

    public void ShowAll() // for showing all
    {
        uiContainer.gameObject.SetActive(true);
    }

    public void Pause() // for pausing game and timer
    {
        PauseMenu.SetActive(true);
        GameStateManager.StopTimer();
        UnlockMouse();
    }

    public void Resume() // for pausing game and timer
    {
        PauseMenu.SetActive(false);
        GameStateManager.ContinueTimer();
        LockMouse();
    }

    public void UnlockMouse() // for pause menu
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;                  
    }

    public void LockMouse() // for pause menu
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;                  
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

        UpdateHumanAbilityNameImage(); // calling name image
    }

    void HAbilityUse() // function for using human ability
    {
        /*GameObject middleSlotIcon = HumanAbilIcons[1]; // slot 2 is index 1 in the array

        if (middleSlotIcon.name == "HSkill") // if skill check is in middle slot
        {
            HiveMindAbility ability = HiveMindController.GetAbility(0); // couldnt find skill check for regular player but it does the same thing so works for now
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

        else if (middleSlotIcon.name == "HDecontam") // if decontamination is in middle slot
        {
            Debug.Log("Call Decontamination"); // needs adding
        }

        else if (middleSlotIcon.name == "HDoor") // if door is in middle slot
        {
            DoorController.OpenDoor();
        }
        else
        {
            Debug.Log("No Match"); // for bug testing
        }*/
    }

    void UpdateHumanAbilityNameImage() // function for showing the correct ability name with whatever ability is currently in slot 2
    {

        AbilDecontam.SetActive(false); // deactivating all 3 name images
        AbilHDoor.SetActive(false); // deactivating all 3 name images
        AbilHSkill.SetActive(false); // deactivating all 3 name images

        GameObject middleSlotIcon = HumanAbilIcons[1]; // grabbing slot 2

        switch (middleSlotIcon.name)
        {
            case "HDecontam":
                AbilDecontam.SetActive(true);
                break;
            case "HDoor":
                AbilHDoor.SetActive(true);
                break;
            case "HSkill":
                AbilHSkill.SetActive(true);
                break;
            default:
                break; // show nothing if no match, mainly for testing
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

        UpdateVirusAbilityNameImage(); // calling name image
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
            ContaminationZoneController.ActivateZone();
        }



        else if (middleSlotIcon.name == "VDoor") // if door is in middle slot
        {
            DoorController.OpenDoor();
        }


        else
        {
            Debug.Log("No Match"); // for bug testing
        }
    }

    void UpdateVirusAbilityNameImage() // function for showing the correct ability name with whatever ability is currently in slot 2
    {

        AbilContam.SetActive(false); // deactivating all 3 name images
        AbilVDoor.SetActive(false); // deactivating all 3 name images
        AbilVSkill.SetActive(false); // deactivating all 3 name images

        GameObject middleSlotIcon = VirusAbilIcons[1]; // grabbing slot 2

        switch (middleSlotIcon.name)
        {
            case "VContam":
                AbilContam.SetActive(true);
                break;
            case "VDoor":
                AbilVDoor.SetActive(true);
                break;
            case "VSkill":
                AbilVSkill.SetActive(true);
                break;
            default:
                break; // show nothing if no match, mainly for testing
        }
    }


}
