using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [Header("Open parameters!")]
    [SerializeField] private EletricitySourceController controller;
    [Tooltip("If the door doesn't require a key")]
    [SerializeField] private string doorTag;

    [Header("Door configs")]
    [SerializeField] private bool canOpenWithInteraction;
    [SerializeField] private float timeRemainingOpen;
    [SerializeField] private Vector3 openLocation;
    [SerializeField] private float doorSpeed;
    private bool doorOpen;
    private float currentTime;


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
        if (!(controller != null && controller.HasPower())) return;
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

    public void OpenDoor()
    {
        doorOpen = true;
        currentTime = timeRemainingOpen;
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
}
