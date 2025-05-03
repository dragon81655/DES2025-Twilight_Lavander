using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable, IUnlockable
{
    // Start is called before the first frame update
    [Header("Open parameters!")]
    [SerializeField] private List<EletricitySourceController> controller;
    [Tooltip("If the door doesn't require a key")]
    [SerializeField] private string doorTag;
    [SerializeField] private bool isVoiceActivated;
    [SerializeField] private bool isHiveMindActivated;

    [Header("Door configs")]
    [SerializeField] private bool canOpenWithInteraction;
    [SerializeField] private float timeRemainingOpen;
    [SerializeField] private float openAmount;
    [SerializeField] private GameObject doorR;
    [SerializeField] private GameObject doorL;
    [SerializeField] private float doorSpeed;
    private bool doorOpen;
    private float currentTime;

    public SFXManager SFXManager; // grabbing for SFX

    [Header("Door details")]
    public string doorName;
    [TextArea(2, 3)]
    public string doorDesc;



    private Vector3 closePosR;
    private Vector3 openPosR;
    private Vector3 closePosL;
    private Vector3 openPosL;
    private void Start()
    {
        closePosR= doorR.transform.localPosition;
        openPosR = closePosR + transform.right * openAmount;
        closePosL = doorL.transform.localPosition;
        openPosL = closePosL - transform.right * openAmount;
    }
    public void Interact(GameObject source)
    {
        Debug.Log("Door interaction!");
        if(!CheckDoorPower()) return;
        if (!canOpenWithInteraction) return;
        if (doorTag != null && doorTag != "")
        {
            InventoryController i = source.GetComponent<InventoryController>();
            if (i != null && i.HasItemByTag(doorTag, 1))
            {
                OpenDoor();
            }
        }
        else OpenDoor();

    }

    private bool CheckDoorPower()
    {
        if (controller == null || controller.Count == 0) return true;
        foreach (EletricitySourceController c in controller)
        {
            if (!c.HasPower()) return false;
        }
        return true;
    }

    public int AttemptDoorOpeningByHiveMind(bool human)
    {
        if (!CheckDoorPower()) return 0; //No Power
        if (!IsControllable(human)) return 1; //It's not hive mind controllable
        OpenDoor();
        return 2; //Success
    }

    public bool IsControllable(bool human)
    {
        return (human && isVoiceActivated) || (!human && isHiveMindActivated);
    }
    public void OpenDoor()
    {
        doorOpen = true;
        currentTime = timeRemainingOpen;
        SFXManager.DoorOpenSFX(); // SFX for door opening
    }

    public string GetDoorName()
    {
        return doorName;
    }

    public string GetDoorDesc()
    {
        return doorDesc;
    }

    private void Update()
    {
        if (doorOpen)
        {
            if (doorR.transform.localPosition != openPosR)
                doorR.transform.localPosition = Vector3.Lerp(doorR.transform.localPosition, openPosR, Time.deltaTime * doorSpeed);
            if (doorL.transform.localPosition != openPosL)
                doorL.transform.localPosition = Vector3.Lerp(doorL.transform.localPosition, openPosL, Time.deltaTime * doorSpeed);

            currentTime -= Time.deltaTime;
            if(currentTime <= 0) doorOpen= false;
        }
        else
        {
            if (doorR.transform.localPosition != closePosR)
                doorR.transform.localPosition = Vector3.Lerp(doorR.transform.localPosition, closePosR, Time.deltaTime * doorSpeed);

            if (doorL.transform.localPosition != closePosL)
                doorL.transform.localPosition = Vector3.Lerp(doorL.transform.localPosition, closePosL, Time.deltaTime * doorSpeed);
        }
    }

    public void Unlock()
    {
        if (!CheckDoorPower()) return;
        OpenDoor();
    }
}
