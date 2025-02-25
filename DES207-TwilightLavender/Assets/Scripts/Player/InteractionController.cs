using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour, IInteractorHandler
{
    private GameObject toInteract;
    public void Interact()
    {
        if(toInteract != null)
        {
            toInteract.GetComponent<IInteractable>().Interact(gameObject);
        }
    }

    public IInteractable GetInteractable()
    {
        return toInteract.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable i = other.transform.GetComponent<IInteractable>();
        Debug.Log("Try interact!");
        if(i != null )
        {
            toInteract = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(toInteract == other.gameObject)
        {
            toInteract = null;
        }
    }
}
