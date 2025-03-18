using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMindManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static HiveMindManager instance;

    [SerializeField]
    private SpaceController startingRoom;
    private SpaceController currentRoom;
    [SerializeField]
    private List<HiveMindController> currentRoomControllers;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(LateStart());
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateCurrentRoom(startingRoom);
    }
    public void UpdateCurrentRoom(SpaceController currentRoom)
    {
        if (currentRoom == this.currentRoom) return;
       foreach(HiveMindController controller in currentRoomControllers)
       {
            if(this.currentRoom != null)
            controller.RemoveAllAbilitiesFromSource(this.currentRoom.gameObject);
            controller.AddAbilitiesFromSpaceController(currentRoom);
       }
       this.currentRoom = currentRoom;

    }

    public void AddAbility(HiveMindAbility ability, GameObject gameObject)
    {
        foreach(HiveMindController abilityController in currentRoomControllers)
        {
            if(gameObject == abilityController.gameObject)
            {
               abilityController.AddAbility(ability);
                return;
            }
        }
    }

    public void RemoveAbility(HiveMindAbility ability, GameObject gameObject)
    {
        foreach (HiveMindController abilityController in currentRoomControllers)
        {
            if (gameObject == abilityController.gameObject)
            {
                abilityController.RemoveAbility(ability);
                return;
            }
        }
    }

    public void SwitchAbilities()
    {
        Debug.Log("Switch!");
        List<HiveMindAbility> t = (List<HiveMindAbility>)currentRoomControllers[0].GetHiveMindAbilities();
        List<HiveMindAbility> t2 = (List<HiveMindAbility>)currentRoomControllers[1].GetHiveMindAbilities();
        currentRoomControllers[0].SetHiveMindAbilities(t2);
        currentRoomControllers[1].SetHiveMindAbilities(t);
    }

}
