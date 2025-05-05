using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CastMinigameAbility : HiveMindAbility
{
    private List<MinigamesToSpawn> availableMinigames;
    private MiniGameController controller;
    

    private bool inCooldown = false;
    private float cooldown = 0;
    public CastMinigameAbility(IEnumerable<MinigamesToSpawn> availableMinigames, float cooldown)
    {
        this.availableMinigames = (List<MinigamesToSpawn>) availableMinigames;
        this.cooldown = cooldown;
        abilityDesc = "Prove your skill and attempt to solidify your dominion of the body or attack the current occupant!";
        abilityName = "TAKE OVER!";
    }
    
    public override bool Act(HiveMindController controller)
    {
        if(inCooldown) return false;
        if(availableMinigames != null && availableMinigames.Count > 0)
        {
            int toCast = UnityEngine.Random.Range(0, (int) availableMinigames.Count);
            BaseActivityController t = GameObject.Instantiate(availableMinigames[toCast].controller, availableMinigames[toCast].parent);
            this.controller.AddMiniGame(t);
            t.Init(this.controller, null);
            controller.StartCoroutine(Cooldown());
            return true;
        }
        else
        {
            Debug.LogError("No minigames on the ability");
        }
        return false;
    }
    private IEnumerator Cooldown()
    {
        inCooldown= true;
        yield return new WaitForSeconds(cooldown);
        inCooldown= false;
    }
    public override void Init(HiveMindController controller)
    {
        this.controller = controller.GetComponent<MiniGameController>();
        spriteId = 1;
    }

    public override void Stop(HiveMindController controller)
    {
    }

    public override void Tick(HiveMindController controller)
    {
    }
}
[Serializable]
public struct MinigamesToSpawn
{
    public BaseActivityController controller;
    public Transform parent;

}