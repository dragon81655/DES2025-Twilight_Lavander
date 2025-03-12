using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActivityController : MonoBehaviour
{
    public abstract GameObject GetControllable(MiniGameController source);
    //Starts minigame
    public abstract void Init(MiniGameController source);
    //Resumes minigame after another ends
    public abstract void Resume(int result);
    //Pauses minigame to instantiate other
    public abstract void Pause();
    //Called when the minigame ends
    public abstract void OnFinish();
}
