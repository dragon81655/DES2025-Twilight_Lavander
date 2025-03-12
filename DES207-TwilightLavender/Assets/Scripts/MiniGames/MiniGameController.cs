using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MiniGameController : MonoBehaviour
{
    private Stack<BaseActivityController> controllers = new Stack<BaseActivityController>();

    public void AddMiniGame(BaseActivityController controller)
    {
        if (controllers.Count > 0 && controllers.Peek() == controller)
        {
            Debug.LogError("Sweet lord of stupidity, ask Miguel for help NOW! You just tried to add the same minigame instance TWICE to the stack you moron!");
            return;
        }

        InputManager.instance.UpdateCurrentlyControlled(controllers.Count > 0 ? controllers.Peek().GetControllable(this) : gameObject, controller.GetControllable(this));
        controllers.Push(controller);
    }

    public void OnFinishMiniGame(int result)
    {
        if(controllers.Count > 0)
        {
            GameObject currentlyControlling = controllers.Pop().GetControllable(this);
            InputManager.instance.UpdateCurrentlyControlled(currentlyControlling, controllers.Count > 0 ? controllers.Peek().GetControllable(this) : gameObject);
            if(controllers.Count > 0)
                controllers.Peek().Resume(result);
            Debug.Log("Finished minigame!");
        }
        else
        {
            Debug.LogWarning("No minigame on stack!");
        }
    }

    public void ClearStack()
    {
        controllers.Clear();
    }
}
[Serializable]
public struct PlayerToMinigameController
{
    [HideInInspector]
    public MiniGameController controller;
    public GameObject player;
}