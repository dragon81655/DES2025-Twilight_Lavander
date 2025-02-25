using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    [Header("PlayerData")]
    [SerializeField] private PlayerInfo p1;
    [SerializeField] private PlayerInfo p2;

    [Header("References")]
    [SerializeField] private GameObject humanBody;
    [SerializeField] private GameObject subconscious;


    private void Awake()
    {
        instance= this;
    }
    void Start()
    {
        //p1.inputDevice = InputDataStaticClass.player1Input;
        //p2.inputDevice = InputDataStaticClass.player2Input;

        p1.UpdateController();
        p2.UpdateController();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SwitchChars();
    }
    public void SwitchChars()
    {

        //p1.currentlyControlled.Clear();
        //p2.currentlyControlled.Clear();
        if (p1.currentlyControlledType == ControllableObjects.Body)
        {
            p1.currentlyControlledType = ControllableObjects.Subconscious;
            p1.currentlyControlled = subconscious ;

            p2.currentlyControlledType = ControllableObjects.Body;
            p2.currentlyControlled = humanBody;
        }
        else
        {
            p1.currentlyControlledType = ControllableObjects.Body;
            p1.currentlyControlled = humanBody;

            p2.currentlyControlledType = ControllableObjects.Subconscious;
            p2.currentlyControlled =subconscious ;
        }
        p1.UpdateController();
        p2.UpdateController();
    }


    public void SwitchMiniGame(bool human, GameObject miniGameController)
    {
        if (human)
        {
            p1.currentlyControlled = p1.currentlyControlled == miniGameController ? TypeToController(p1.currentlyControlledType) : miniGameController;
            p1.UpdateController() ;
        }
        else
        {
            p2.currentlyControlled = p2.currentlyControlled == miniGameController ? TypeToController(p2.currentlyControlledType) : miniGameController;
            p2.UpdateController() ;
        }

    }

    //If the object returns true, it's the human, otherwise it's the virus
    public bool CheckObjectRole(GameObject obj)
    {
        return obj == p1.currentlyControlled;
    }

    private GameObject TypeToController(ControllableObjects currentObject)
    {
        if (currentObject == ControllableObjects.Body) return humanBody;
        else return subconscious;
    }
}

[Serializable]
public class PlayerInfo
{
    

    public bool isHuman;
    public string inputDevice;
    public InputController inputController;
    public GameObject currentlyControlled;
    public ControllableObjects currentlyControlledType;

    public void UpdateController()
    {
        inputController.SetInputType(inputDevice);
        inputController.SwitchTarget(currentlyControlled);
    }
    
}
public enum ControllableObjects
{
    Body = 0,
    Subconscious
}
