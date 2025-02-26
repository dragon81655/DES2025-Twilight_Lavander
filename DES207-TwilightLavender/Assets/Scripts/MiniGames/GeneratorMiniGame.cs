using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorMiniGame : MinigameBase, IInteractorHandler
{
    [SerializeField] private List<MinigameBase> possibleGames;


    [SerializeField] private bool isFinished = false;

    private float totalTimer;
    private float timer;
    private float nextMiniGameTimer;

    private bool runTimer = false;

    public void Damage(float time)
    {
        totalTimer = time;
    }

    public override bool Finish(object param)
    {
        return isFinished;
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            timer -= Time.deltaTime;
            nextMiniGameTimer= -Time.time;
            if (timer <= 0)
            {
                runTimer = false;
                InputManager.instance.SwitchMiniGame(gameObject, gameObject);
            }
            if (nextMiniGameTimer <= 0)
            {
                runTimer = false;
                //Minigame code here
            }
            
        }
    }

    private void NextMiniGameRandomizer()
    {

    }

    public void AddToTimer(float time)
    {
        timer += time;
    }

    public void Interact()
    {
        runTimer = false;
        InputManager.instance.SwitchMiniGame(gameObject, gameObject);
    }

    public override void Init(GameObject creator, object param)
    {
        timer = totalTimer;
        runTimer = true;
    }

    public override void Pause()
    {
    }

    public override void Resume()
    {
    }
}
