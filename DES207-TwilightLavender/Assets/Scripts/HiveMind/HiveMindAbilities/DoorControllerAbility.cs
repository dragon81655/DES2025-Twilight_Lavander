using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerAbility : HiveMindAbility
{
    private string abName = "Control door";
    private string abDes = "Opens door if powered:\nDoor:\n";


    private DoorController controller;
    public void AssociateDoor(DoorController controllerD)
    {
        this.controller = controllerD;
        abilityDesc += controllerD.doorName + "\nDoor desc:\n" + controllerD.doorDesc;
    }
    public override bool Act(HiveMindController controller)
    {
        if(this.controller != null)
        { 
            this.controller.AttemptDoorOpeningByHiveMind(!InputManager.instance.isVirusWithBase(controller.gameObject));
            return true;
        }
        else
        {
            Debug.LogError("Door controller not associated in " + controller.gameObject.name);
        }
        return false;
    }

    public override void Init(HiveMindController controller)
    {
        abilityName = abName;
        abilityDesc = abDes;
    }

    public override void Stop(HiveMindController controller)
    {
    }

    public override void Tick(HiveMindController controller)
    {
    }

    public DoorControllerAbility(GameObject source)
    {
        this.source = source;
    }
}
