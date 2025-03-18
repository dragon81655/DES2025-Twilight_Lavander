using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricitySourceController : MonoBehaviour, IInteractable,IDamageable, IMiniGameDependent
{
    [SerializeField] private bool hasPower;

    [Header("Minigame related!")]
    [SerializeField] private float interactionTotalDamage;
    [SerializeField] private BaseActivityController miniGameController;
    [SerializeField] private Transform canvas;

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
                MiniGameController mc = source.GetComponent<MiniGameController>();
                BaseActivityController mg = Instantiate(miniGameController, canvas);
                mc.AddMiniGame(mg);
                mg.Init(mc, this);
            }
            else Damage(0);
        }
    }
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.material = hasPower? workingMat : notWorkingMat;
    }

    public void Repair()
    {
        hasPower= true;
        mesh.material = hasPower ? workingMat : notWorkingMat;
    }

    public void Notify(int result)
    {
        if(result == 1)
            Repair();
    }

    public void Damage(float secondsToRepair)
    {
        hasPower = false;
        mesh.material = hasPower ? workingMat : notWorkingMat;
    }
}
