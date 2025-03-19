using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    // UI Elements

    public Image Bar; // grabbing the bar
    public TextMeshProUGUI TimerText; // grabbing timer text
    public TextMeshProUGUI AbilTimerText; // grabbing ability timer text
    public GameObject DialogueContainer; // grabbing dialogue box
    public GameObject uiContainer; // grabbing UI
    public TextMeshProUGUI VirusWinsText; // grabbing virus wins text
    public TextMeshProUGUI HumanWinsText; // grabbing human wins text
    public Image AbilityDown; // grabbing ability
    public TextMeshProUGUI InteractText; // grabbing interact text

    // Takeover Timer Settings

    private float switchTimerB; // getting time limit for bar
    private float switchTimerValueB; // getting time remaining for bar

    // Ability Timer Settings

    public float CooldownTime = 5f; // time taken for the ability to be usable again
    public float CurrentCD; // track time remaining
    private bool ActiveCD; // for disabling input while cooldown is active

    // Misc

    public bool InteractRange; // whether or not player is within range to interact
    public bool MenuOpen; // check whether dialogue menu is open

    // Other Scripts

    ProxInteraction ProxInteractionScript; // calling prox interaction script
    InventoryController invController; // calling inventory controller script
    public GameStateManager GameStateManager; // grabbing game state manager script

    // Start is called before the first frame update
    void Start()

    {
        CurrentCD = 0f; // setting ability CD to 0 at start
        ActiveCD = false; // making sure input isnt locked out from start

        VirusWinsText.gameObject.SetActive(false); // ensuring virus wins is disabled from start
        HumanWinsText.gameObject.SetActive(false); // ensuring human wins is disabled from start
        AbilityDown.gameObject.SetActive(false); // ensuring ability cd is disabled from start

        ProxInteractionScript = GameObject.FindGameObjectWithTag("uiTag").GetComponent<ProxInteraction>(); // calling interaction script
        invController = GetComponent<InventoryController>(); // calling inventory script

        float switchTimerB = GameStateManager.instance.GetSwitchTimer(); // setting switch timer to gamestatemanager declared value
        Debug.Log("Switch Timer: " + switchTimerB); // for testing

        float switchTimerValueB = GameStateManager.instance.GetCurrentSwitchTimer(); // setting current switch timer to gamestatemanager declared value
        Debug.Log("Current Switch Timer: " + switchTimerValueB); // for testing
    }

    // Update is called once per frame
    void Update()

    {
        float switchTimerValueB = GameStateManager.instance.GetCurrentSwitchTimer(); // updating current switch time for bar
        float switchTimerB = GameStateManager.instance.GetSwitchTimer(); // updating total time for bar

        if (switchTimerValueB > 0) // if timer is above 0s
        {
            Bar.fillAmount = switchTimerValueB / switchTimerB; // start bar drain
        }
        else
        {
            Bar.fillAmount = 0; // set to zero
        }

        int minutes = Mathf.FloorToInt(switchTimerValueB / 60); // converting to mm:ss
        int seconds = Mathf.FloorToInt(switchTimerValueB % 60); // converting to mm:ss
        TimerText.text = $"{minutes:00}:{seconds:00}"; // displaying timer in mm:ss
        // Debug.Log("Current Switch Timer: " + switchTimerValueB); // FOR TESTING

        if (CurrentCD > 0) // for ability timer
            CurrentCD -= Time.deltaTime;

        else
            AbilityDown.gameObject.SetActive(false);

        AbilTimerText.text = $"{CurrentCD:F1}s"; // display cd timer
    }

    private void FixedUpdate()

    {
        if (Input.GetKeyDown(KeyCode.F) && !ActiveCD) // if F key is pressed
            AbilityUsed(); // call ability use function

        if (CurrentCD <= 0)
            ActiveCD = false; // checking to see if CD is up to restore input

        if (ActiveCD == false) // for enabling and disabling visual ability timer
            AbilTimerText.gameObject.SetActive(false);
        else
            AbilTimerText.gameObject.SetActive(true);

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

    void AbilityUsed() // function for ability being used

    {
        AbilityDown.gameObject.SetActive(true); // enable CD image
        ActiveCD = true; // locking out input
        CurrentCD = CooldownTime; // set cooldown
    }

    public void Interact() // interaction function

    {
        InteractRange = true;
        if (Input.GetKeyDown(KeyCode.E) && InteractRange == true) // condition for opening dialoge box
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
}
