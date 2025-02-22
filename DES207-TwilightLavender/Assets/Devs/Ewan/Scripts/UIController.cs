using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using JetBrains.Annotations;

public class UIController : MonoBehaviour
{
    // UI Elements

    public Image Bar; // grabbing the bar
    public TextMeshProUGUI TimerText; // grabbing timer text
    public TextMeshProUGUI AbilTimerText; // grabbing ability timer text
    public GameObject DialogueContainer; // grabbing dialogue box
    public GameObject uiContainer; // grabbing UI
    public TextMeshProUGUI VirusWinsText; // grabbing virus wins text
    public Image AbilityDown; // grabbing ability
    public TextMeshProUGUI InteractText; // grabbing interact text

    // Takeover Timer Settings

    public float DepletionTime; // time taken for the bar to deplete
    public float CurrentTime; // track time remaining

    // Ability Timer Settings

    public float CooldownTime = 5f; // time taken for the ability to be usable again
    public float CurrentCD; // track time remaining
    private bool ActiveCD; // for disabling input while cooldown is active

    // Misc

    public bool InteractRange; // whether or not player is within range to interact
    public bool MenuOpen; // check whether dialogue menu is open
    ProxInteraction ProxInteractionScript; // calling other script

    // Start is called before the first frame update
    void Start()

    {
        CurrentTime = DepletionTime; // for takeover timer
        CurrentCD = 0f; // setting ability CD to 0 at start
        ActiveCD = false; // making sure input isnt locked out from start

        VirusWinsText.gameObject.SetActive(false); // ensuring virus wins is disabled from start
        AbilityDown.gameObject.SetActive(false); // ensuring ability cd is disabled from start

        ProxInteractionScript = GameObject.FindGameObjectWithTag("uiTag").GetComponent<ProxInteraction>(); // calling interaction script
    }

    // Update is called once per frame
    void Update()

    {
        if (CurrentTime > 0) // for takeover timer
            CurrentTime -= Time.deltaTime;
        Bar.fillAmount = CurrentTime / DepletionTime; // calculating bar depletion rate

        int minutes = Mathf.FloorToInt(CurrentTime / 60); // get whole minutes
        int seconds = Mathf.FloorToInt(CurrentTime % 60); // get remaining seconds
        TimerText.text = $"{minutes:00}:{seconds:00}"; // converting to mm:ss

        if (CurrentCD > 0) // for ability timer
            CurrentCD -= Time.deltaTime;

        else
            AbilityDown.gameObject.SetActive(false);

        AbilTimerText.text = $"{CurrentCD:F1}s"; // display cd timer
    }

    private void FixedUpdate()

    {
        if (CurrentTime < 1) // check if time is out
            Invoke("VirusWins", 0.8f); // call virus win state after delay

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

    void AbilityUsed() // function for ability being used

    {
        AbilityDown.gameObject.SetActive(true); // enable CD image
        ActiveCD = true; // locking out input
        CurrentCD = CooldownTime; // set cooldown
    }
    
    public void Interact() // interaction function

    {
        InteractRange = true;
        if (Input.GetKeyDown(KeyCode.E) && InteractRange == true)
        {
            DialogueContainer.SetActive(true);
            MenuOpen = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && MenuOpen == true)
        {
            DialogueContainer.SetActive(false);
            MenuOpen = false;
        }
    }

}
