using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameWorldEvoker : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    [SerializeField] private List<MinigamesToSpawn> possibleControllers;

    public bool isActive = true;

    public void Interact(GameObject source)
    {
        if(!isActive) return;
        MinigamesToSpawn mg = possibleControllers[Random.Range(0, possibleControllers.Count)];
        BaseActivityController toStart = Instantiate(mg.controller, mg.parent);
        MiniGameController mc = source.GetComponent<MiniGameController>();
        mc.AddMiniGame(toStart);
        toStart.Init(mc, null);
    }
}
