using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
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
        foreach (PlayerInfo player in players)
        {
            player.UpdateController();
        }
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

    public bool isVirus(GameObject target)
    {
        return target == players[1].currentlyControlling;
    }
}

[Serializable]
public class PlayerInfo
{
    public string inputDevice;
    public InputController inputController;
    public GameObject currentlyControlling;
    //public Stack<BaseActivityController> currentlyControlled = new Stack<BaseActivityController>();

    public void UpdateController()
    {
        inputController.SetInputType(inputDevice);
        inputController.SwitchTarget(currentlyControlling);
    }
}
