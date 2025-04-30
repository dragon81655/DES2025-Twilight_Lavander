using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTesterControlls : MonoBehaviour
{
    // Start is called before the first frame update
    InputController controller;
    [SerializeField]
    private GameObject activityController;


    void Start()
    {
        controller = GetComponent<InputController>();
        controller.SwitchTarget(activityController);
    }
}
