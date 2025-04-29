using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable, IUnlockable
{
    // Start is called before the first frame update
    [Header("Open parameters!")]
    [SerializeField] private EletricitySourceController controller;
    [Tooltip("If the door doesn't require a key")]
    [SerializeField] private string doorTag;
    [SerializeField] private bool isVoiceActivated;
    [SerializeField] private bool isHiveMindActivated;

    [Header("Door configs")]
    [SerializeField] private bool canOpenWithInteraction;
    [SerializeField] private float timeRemainingOpen;
    [SerializeField] private Vector3 openLocation;
    [SerializeField] private float doorSpeed;
    private bool doorOpen;
    private float currentTime;

    [Header("Door details")]
    public string doorName;
    [TextArea(2, 3)]
    public string doorDesc;

    private Vector3 closePos;
    private Vector3 openPos;
    private void Start()
    {
        closePos= transform.localPosition;
        openPos = openLocation;
    }
    public void Interact(GameObject source)
    {
        Debug.Log("Door interaction!");
        if (!(controller == null || controller.HasPower())) return;
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

    public int AttemptDoorOpeningByHiveMind(bool human)
    {
        if (controller != null && !controller.HasPower()) return 0; //No Power
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
            if (transform.localPosition != openPos)
                transform.localPosition = Vector3.Lerp(transform.localPosition, openPos, Time.deltaTime * doorSpeed);
            currentTime -= Time.deltaTime;
            if(currentTime <= 0) doorOpen= false;

        }
        else
        {
            if(transform.localPosition != closePos)
                transform.localPosition = Vector3.Lerp(transform.localPosition, closePos, Time.deltaTime * doorSpeed);
        }
    }

    public void Unlock()
    {
        if ((controller != null && controller.HasPower()) || controller == null) return;
        OpenDoor();
    }
}
