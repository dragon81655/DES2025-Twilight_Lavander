using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbilityUser : MonoBehaviour, IUseable0
{
    private HiveMindController controller;
    [SerializeField] private int index;
    public void Use0()
    {
        controller.UseAbility(index);
    }

    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<HiveMindController>();
    }
}
