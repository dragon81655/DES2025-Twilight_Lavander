using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoInteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject controller;
    public void Interact(GameObject source)
    {
        GameObject t = Instantiate(controller);
        BaseActivityController mg = t.GetComponent<BaseActivityController>();
        if (mg != null)
        {
            MiniGameController mgc = source.GetComponent<MiniGameController>();
            mg.Init(mgc, null);

            mgc.AddMiniGame(mg);
            GameObject.Find("Subconscious").GetComponent<MiniGameController>().AddMiniGame(mg);
        }
        else Debug.LogWarning("No BaseActivityController on prefab");
    }
}
