using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryGeneratorGame : MonoBehaviour
{

    [SerializeField] private Image progress;
    [SerializeField] private GameObject pressInteract;
    [SerializeField] private EletricitySourceController toCall;

    private float totalTime;
    private float time;

    private bool human;
    

    public void Init(EletricitySourceController obj, float time, bool human)
    {
        toCall= obj;
        if(time > 0) totalTime= time;
        this.time = totalTime;
        progress.fillAmount= 1;
        this.human = human;
    }
    

    private void FinishGame()
    {
        pressInteract.SetActive(false);
        toCall.Repair();
        gameObject.SetActive(false);
        InputManager.instance.SwitchMiniGame(human, gameObject);
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            FinishGame();
        }
        progress.fillAmount = time/totalTime;
    }
}
