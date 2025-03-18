using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [SerializeField] private UnityEvent onSwitch;
    [SerializeField] private UnityEvent onSwitchRequest;

    [Header("PlayerData")]
    [SerializeField] private PlayerInfo[] players;

    [Header("References")]
    [SerializeField] private GameObject humanBody;
    [SerializeField] private GameObject subconscious;

    private bool lockChange = false;
    private bool requestChange = false;

    private void Awake()
    {
        instance= this;
    }
    void Start()
    {
        //p1.inputDevice = InputDataStaticClass.player1Input;
        //p2.inputDevice = InputDataStaticClass.player2Input;
        foreach(PlayerInfo player in players)
        {
            player.UpdateController();
            player.baseControlling = player.currentlyControlling;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) requestChange = true;

        if(requestChange)
        {
            if (!lockChange)
            {
                requestChange = false;
                SwitchChars();
            }
        }
    }

    private void SwitchChars()
    {
        GameObject t = players[0].currentlyControlling;
        players[0].currentlyControlling = players[1].currentlyControlling;
        players[1].currentlyControlling = t;

        GameObject t2 = players[0].baseControlling;
        players[0].baseControlling = players[1].baseControlling;
        players[1].baseControlling = t;
        foreach (PlayerInfo player in players)
        {
            player.UpdateController();
        }
        onSwitch.Invoke();
    }
    public void LockSwitch()
    {
        lockChange = true;
    }
    public void UnlockSwitch()
    {
        lockChange = false;
    }
    public void RequestSwitchChars()
    {
        requestChange= true;
        onSwitchRequest.Invoke();

    }

    public void UpdateCurrentlyControlled(GameObject source, GameObject newControllable)
    {
        foreach(PlayerInfo target in players)
        {
            if(target.currentlyControlling == source)
            {
                target.currentlyControlling = newControllable;
                target.UpdateController();
                Debug.Log("Updated controllers!");
                return;
            }
        }
    }

    public string GetInputType(GameObject target)
    {
        foreach (PlayerInfo player in players)
        {
            if(player.currentlyControlling == target)
            {
                return player.inputDevice;
            }
        }
        Debug.LogError("Tried to obtain the input device of a non bound object");
        return "";
    }

    public bool isVirusWithCurrent(GameObject target)
    {
        return target == players[1].currentlyControlling;
    }

    public bool isVirusWithBase(GameObject target)
    {
        return target == players[1].baseControlling;

    }
}

[Serializable]
public class PlayerInfo
{
    public string inputDevice;
    public InputController inputController;
    public GameObject currentlyControlling;
    public GameObject baseControlling;

    public void UpdateController()
    {
        inputController.SetInputType(inputDevice);
        inputController.SwitchTarget(currentlyControlling);
    }
}
