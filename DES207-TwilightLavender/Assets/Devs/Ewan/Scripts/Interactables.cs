using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{
    UIController UIController; // grabbing UI controller script

    public GameObject Player; // grabbing player

    public Image DialogueContainer; // grabbing dialogue container
    public Image[] DialogueSequence; // for stringing dialogue together
    // public Image LabReport; // grabbing lab report sprite

    public TextMeshProUGUI EToInteract; // grabbing interact enable text

    public float ProxRange = 3f; // range for allowing interaction
    private int CurrentDialogueIndex = 0; // tracking which dialogue is present on screen

    public bool MenuOpen; // for checking if menu is open
    public bool CanInteract; // for checking if interact is eligible


    // Start is called before the first frame update
    void Start()
    {
        UIController = GameObject.FindGameObjectWithTag("uiTag").GetComponent<UIController>(); // for disabling rest of UI while dialogue is present
        Player = GameObject.Find("Player");
        foreach (Image img in DialogueSequence)
        {
            img.gameObject.SetActive(false);
        }

        MenuOpen = false; // setting menu open to false
        CanInteract = false; // making sure you cant open menu off start
        // LabReport.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position); // checking distance between player and object

        if (distance <= ProxRange && MenuOpen == false)
        {
            CanInteract = true;
        }

        else
        {
            CanInteract = false;
        }

        if (CanInteract)
        {
            EToInteract.gameObject.SetActive(true);
        }
        else
        {
            EToInteract.gameObject.SetActive(false);
        }

        if (distance >= ProxRange && MenuOpen == true)
        {
            MenuOpen = false;
            MenuClosed();
        }

        if (CanInteract == true && Input.GetKeyDown(KeyCode.E))
        {
            MenuOpen = true;
            MenuOpened();
        }

        if (MenuOpen == true && Input.GetMouseButtonDown(0)) // calling function to progress dialogue
        {
            AdvanceDialogue();
        }
    }

    public void MenuOpened() // function for enabling all relevant dialogue menu elements
    {
        UIController.Pause();
        UIController.HideAll();

        foreach (Image img in DialogueSequence)
        {
            img.gameObject.SetActive(false);
        }

        CurrentDialogueIndex = 0;
        ShowDialogueImage(CurrentDialogueIndex);

        EToInteract.gameObject.SetActive(false);
        DialogueContainer.gameObject.SetActive(true);
        // LabReport.gameObject.SetActive(true);
    }

    public void MenuClosed() // function for disabling all relevant dialogue menu elements
    {
        UIController.Resume();
        UIController.ShowAll();

        DialogueContainer.gameObject.SetActive(false);
        // LabReport.gameObject.SetActive(false);
        foreach (Image img in DialogueSequence)
        {
            img.gameObject.SetActive(false);
        }
    }

    public void ShowDialogueImage(int index)
    {
        for (int i = 0; i < DialogueSequence.Length; i++)
        {
            DialogueSequence[i].gameObject.SetActive(i == index);
        }
    }

    public void AdvanceDialogue()
    {
        CurrentDialogueIndex++;
        if (CurrentDialogueIndex >= DialogueSequence.Length)
        {
            MenuOpen = false;
            MenuClosed();
        }
        else
        {
            ShowDialogueImage(CurrentDialogueIndex);
        }
    }
}
