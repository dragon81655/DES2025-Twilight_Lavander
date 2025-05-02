using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMindController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<HiveMindAbility> hiveMindAbilities;
    [SerializeField]
    private List<MinigamesToSpawn> minigamesToSpawn;

    [SerializeField]
    private bool isHuman;

    public bool IsHuman => isHuman;

    private void Awake()
    {
        if (hiveMindAbilities == null) hiveMindAbilities = new List<HiveMindAbility>();
        if(minigamesToSpawn != null && minigamesToSpawn.Count > 0)
        {
            hiveMindAbilities.Add(new CastMinigameAbility(minigamesToSpawn, 10));
        }
        if (hiveMindAbilities != null && hiveMindAbilities.Count > 0)
        {
            foreach (HiveMindAbility ab in hiveMindAbilities)
            {
                if (ab != null)
                {
                    ab.Init(this);
                }
            }
        }
    }
    public IEnumerable<HiveMindAbility> GetHiveMindAbilities()
    {
        return hiveMindAbilities;
    }
    public void SetHiveMindAbilities(IEnumerable<HiveMindAbility> value)
    {
        hiveMindAbilities = (List<HiveMindAbility>)value;
    }
    public HiveMindAbility GetAbility(int index)
    {
        Debug.Log("Index: " + index + "\nCount: " + hiveMindAbilities.Count);
        if (index >= hiveMindAbilities.Count) return null;
        return hiveMindAbilities[index];
    }
    public int GetAbilityCount()
    {
        return hiveMindAbilities.Count;
    }
    public void UseAbility(int index)
    {
        if (index >= hiveMindAbilities.Count)
        {
            Debug.LogError("Out of bonds on hivemind abilities");
            return;
        }

        hiveMindAbilities[index].Act(this);
    }

    public void StopAbility(int index)
    {
        if (index >= hiveMindAbilities.Count)
        {
            Debug.LogError("Out of bonds on hivemind abilities");
            return;
        }
        hiveMindAbilities[index].Stop(this);
    }

    public void RemoveAllAbilitiesFromSource(GameObject source)
    {
        if(hiveMindAbilities != null && hiveMindAbilities.Count > 0)
        hiveMindAbilities.RemoveAll(ab => ab.source == source);
    }

    public void AddAbilitiesFromSpaceController(SpaceController spaceController)
    {
        Debug.Log("Try to update controller");
        List<DoorController> doors = (List<DoorController>)spaceController.GetHiveMindDoors(!InputManager.instance.isVirusWithBase(gameObject)) ;
        List<ContaminationZoneController> czs = (List<ContaminationZoneController>)spaceController.GetContaminationZones(!InputManager.instance.isVirusWithBase(gameObject));

        if (doors != null && doors.Count > 0)
        {
            foreach (DoorController door in doors)
            {
                DoorControllerAbility ab = new DoorControllerAbility(spaceController.gameObject);
                ab.Init(this);
                ab.AssociateDoor(door);
                hiveMindAbilities.Add(ab);
            }
        }

        if (czs != null && czs.Count > 0) {
            foreach (ContaminationZoneController cz in czs)
            {
                ContaminationZoneAbility ab = new ContaminationZoneAbility(spaceController.gameObject);
                ab.Init(this);
                ab.AssociateController(cz);
                hiveMindAbilities.Add(ab);
            }
        }
    }
    public void AddAbility(HiveMindAbility ability)
    {
        if (!hiveMindAbilities.Contains(ability))
            hiveMindAbilities.Add(ability);
        else Debug.LogError("You can't add the same ability twice!");
    }

    public void RemoveAbility(HiveMindAbility ability)
    {
        if (hiveMindAbilities.Contains(ability))
            hiveMindAbilities.Remove(ability);
        else Debug.LogWarning("Ability isn't present");
    }


    // Update is called once per frame
    void Update()
    {
        if (hiveMindAbilities != null && hiveMindAbilities.Count > 0)
        {
            foreach (HiveMindAbility ab in hiveMindAbilities)
            {
                ab.Tick(this);
            }
        }
    }
}
