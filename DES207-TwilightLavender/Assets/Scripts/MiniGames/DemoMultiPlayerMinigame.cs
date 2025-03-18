using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMultiPlayerMinigame : BaseActivityController
{
    [SerializeField] private StatusEffectsBase winnerEffect;

    [SerializeField] private PlayerToMinigameController p1 = new PlayerToMinigameController();
    [SerializeField] private PlayerToMinigameController p2 = new PlayerToMinigameController();

    private MiniGameController creator;
    //For this demo using 0 as player 1 wins and 1 as player 2 wins
    private int result;
    public override GameObject GetControllable(MiniGameController source)
    {
        if (p1.controller == null)
        {
            p1.controller = source;
            return p1.player;
        }
        else if (p2.controller == null)
        {
            p2.controller = source;
            return p2.player;
        }
        else
        {
            return p1.controller == source ? p1.player : p2.player;
        }

    }

    public override void Init(MiniGameController source, IMiniGameDependent objs)
    {
        creator = source;
        GameStateManager.instance.PauseSwitchTimer();
    }

    public override void OnFinish()
    {
        p1.controller.OnFinishMiniGame(result);
        p2.controller.OnFinishMiniGame(result);
        Debug.Log("The " + (result == 0 ? "player1" : "player2") + " wins the minigame!");
        if(p1.controller.TryGetComponent(out StatusEffectsController secController))
        {
            secController.AddEffect(winnerEffect, gameObject);
        }
        GameStateManager.instance.ContinueSwitchTimer();
        Destroy(gameObject);
    }
    public override void Pause()
    {
    }

    public override void Resume(int result)
    {
    }

    public void SetResult(int result)
    {
        this.result = result;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
