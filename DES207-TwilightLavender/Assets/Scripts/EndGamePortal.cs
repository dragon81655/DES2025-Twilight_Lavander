using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePortal : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] private Material openMat;
    [SerializeField] private Material closeMat;

    private MeshRenderer mr;
    private bool state = false;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    public void SwitchPortalState(bool state)
    {
        if (mr == null) return;
        this.state = state;
        if(openMat!= null && closeMat != null) 
        mr.material = this.state ? openMat : closeMat;
    }

    public void Interact(GameObject source)
    {
        if (state)
        Debug.Log("Human wins!");
    }
}
