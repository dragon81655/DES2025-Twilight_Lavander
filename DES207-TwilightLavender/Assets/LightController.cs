using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Light light;
    private EletricitySourceController eletricitySourceController;
    bool currentState = true;
    void Start()
    {
        eletricitySourceController = GetComponent<EletricitySourceController>();
        currentState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(eletricitySourceController.HasPower() != currentState)
        {
            if (currentState)
            {
                
            }
        }
    }
}
