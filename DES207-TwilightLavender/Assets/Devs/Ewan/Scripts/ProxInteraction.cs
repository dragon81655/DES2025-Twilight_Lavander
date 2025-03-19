using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProxInteraction : MonoBehaviour
{
    UIController UIControllerScript;

    public GameObject Player;  // grabbing player
    public GameObject Object;    // grabbing object
    public float proximityDistance = 5f;  // proximity range

    // Start is called before the first frame update
    void Start()
    {
        UIControllerScript = GameObject.FindGameObjectWithTag("TestTag").GetComponent<UIController>(); // getting access to UI script
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, Object.transform.position); // checking distance between player and object

        if (distance <= proximityDistance)
        {
            UIControllerScript.InteractText.gameObject.SetActive(true);
            UIControllerScript.Interact(); // calling interact function from UI Script
        }

        else
        {
            UIControllerScript.InteractText.gameObject.SetActive(false);
            UIControllerScript.InteractRange = false;
        }
    }
}
