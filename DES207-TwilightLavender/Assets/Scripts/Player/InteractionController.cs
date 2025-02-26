using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour, IInteractorHandler
{
    private GameObject toInteract;

    [SerializeField]
    private List<GameObject> interactables = new List<GameObject>();
    
    public void Interact()
    {
        CheckOtherInteractables();
        if(toInteract!= null)
        toInteract.GetComponent<IInteractable>().Interact(gameObject);
        
        
    }
    private void CheckOtherInteractables()
    {
        if (interactables.Count > 0)
        {
            toInteract= interactables[0];
            interactables.Remove(toInteract);
        }
    }
    public IInteractable GetInteractable()
    {
        return toInteract.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable i = other.transform.GetComponent<IInteractable>();
        if(i != null )
        {
            interactables.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(toInteract == other.gameObject)
        {
            toInteract = null;
        }
        else
        {
            if (interactables.Contains(other.gameObject))
            {
                interactables.Remove(other.gameObject);
            }
        }
    }
}
