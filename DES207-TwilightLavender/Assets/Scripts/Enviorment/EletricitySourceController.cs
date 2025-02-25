using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricitySourceController : MonoBehaviour, IInteractable, IDamageable
{
    // Start is called before the first frame update~~
    [SerializeField] private bool hasPower;

    [Header("Minigame related!")]
    [SerializeField] private float interactionTotalDamage;
    [SerializeField] private GeneratorMiniGame miniGameController;

    public bool HasPower()
    {
        return hasPower;
    }

    public void Interact(GameObject source)
    {
        if(InputManager.instance!= null)
        {
            if (hasPower)
            {
                /*miniGameController.Init(null);
                InputManager.instance.SwitchMiniGame(source, miniGameController.gameObject);*/
            }
            else Damage(interactionTotalDamage);
        }
    }

    public void Damage(float secondsToRepair)
    {
        hasPower = false;
        miniGameController.Damage(secondsToRepair);
    }

    public void Repair()
    {
        hasPower= true;
    }
}
