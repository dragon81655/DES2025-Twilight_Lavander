using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricitySourceController : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] private bool hasPower;

    [Header("Minigame related!")]
    [SerializeField] private float interactionTotalDamage;
    [SerializeField] private TemporaryGeneratorGame miniGameController;

    [SerializeField]private float damageAmount;

    [SerializeField] private Material workingMat;
    [SerializeField] private Material notWorkingMat;

    private MeshRenderer mesh;
    public bool HasPower()
    {
        return hasPower;
    }

    public void Interact(GameObject source)
    {
        if(InputManager.instance!= null)
        {
            if (!hasPower)
            {
                miniGameController.gameObject.SetActive(true);
                miniGameController.Init(this, damageAmount, InputManager.instance.CheckObjectRole(source));
                InputManager.instance.SwitchMiniGame(InputManager.instance.CheckObjectRole(source), miniGameController.gameObject);
            }
            else Damage(interactionTotalDamage);
        }
    }
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = hasPower? workingMat : notWorkingMat;
    }
    public void Damage(float secondsToRepair)
    {
        hasPower = false;
        damageAmount= secondsToRepair;
        mesh.material = hasPower ? workingMat : notWorkingMat;
    }

    public void Repair()
    {
        hasPower= true;
        mesh.material = hasPower ? workingMat : notWorkingMat;
    }
}
