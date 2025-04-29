using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSwitcherController : MonoBehaviour
{
    [SerializeField] private SpaceController spaceController;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        HiveMindManager.instance.UpdateCurrentRoom(spaceController);
    }
}
