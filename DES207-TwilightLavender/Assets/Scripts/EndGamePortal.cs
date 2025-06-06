using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePortal : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] private Material openMat;
    [SerializeField] private Material closeMat;
    UIController uiController; // grabbing UI Controller

    private MeshRenderer mr;
    private bool state = false;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();

        GameObject t = GameObject.FindGameObjectWithTag("uiTag");
        if (t)
            uiController = t.GetComponent<UIController>(); // grabbing UI Controller
        else Debug.LogWarning("No UI Controller in scene!");
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
        {
            Debug.Log("Human wins!");
            SceneManager.LoadScene("HumanWin");
        }
    }
}
