using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Lights")]
    [SerializeField] private List<Light> lights;
    [Header("Doors")]
    [SerializeField] private List<DoorController> doors;
    [Header("Contamination zones")]
    [SerializeField] private List<ContaminationZoneController> decontaminationZones;
    [SerializeField] private List<ContaminationZoneController> sporeZones;

    public IEnumerable<DoorController> GetHiveMindDoors(bool human)
    {
        List<DoorController> doorControllers = new List<DoorController>(doors);
        doorControllers.RemoveAll(d => !d.IsControllable(human));
        return doorControllers;
    }

    public IEnumerable<ContaminationZoneController> GetContaminationZones(bool human)
    {
        return human ? decontaminationZones: sporeZones;
    }
}
