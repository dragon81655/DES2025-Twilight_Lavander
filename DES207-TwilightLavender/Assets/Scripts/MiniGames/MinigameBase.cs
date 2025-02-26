using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    public abstract void Init(GameObject creator, object param);

    public abstract void Pause();
    public abstract void Resume();

    public abstract bool Finish(object param);
}
