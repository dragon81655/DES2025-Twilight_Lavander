using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaminationZoneAbility : HiveMindAbility
{
    private ContaminationZoneController czController;
    public void AssociateController(ContaminationZoneController czController)
    {
        this.czController = czController;
    }
    public override bool Act(HiveMindController controller)
    {
        if(czController!= null)
        {
            czController.ActivateZone();
            return true;
        }
        Debug.LogError("Contamination Zone Controller not associated");
        return false;
    }

    public override void Init(HiveMindController controller)
    {
    }

    public override void Stop(HiveMindController controller)
    {
    }

    public override void Tick(HiveMindController controller)
    {
    }

    public ContaminationZoneAbility(GameObject source)
    {
        this.source = source;
    }
}
