using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMindActivator : MonoBehaviour, IHiveMindSummoner
{
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject prefabMenu;
    private MiniGameController miniGameController;
    public void Summon()
    {
        Debug.Log("Summon!");
        HiveMindUIController t = Instantiate(prefabMenu, canvas).GetComponent<HiveMindUIController>();
        miniGameController.AddMiniGame(t);
        t.Init(miniGameController);
    }

    private void Start()
    {
        miniGameController = GetComponent<MiniGameController>();

    }
}
