using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputController p1;
    [SerializeField] private InputController p2;
    [SerializeField] private GameObject humanBody;
    void Start()
    {
        p1.SwitchTarget(humanBody, "GP");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
