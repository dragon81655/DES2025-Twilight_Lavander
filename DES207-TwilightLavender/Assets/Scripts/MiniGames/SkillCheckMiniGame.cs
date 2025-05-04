using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SkillCheckMiniGame : BaseActivityController, IInteractorHandler
{
    private MiniGameController source;

    SFXManager SFXManager; // for sfx

    private int result = -1;

    [Header("Objects")]
    [SerializeField] private GameObject needle;

    [Header("Parameters")]
    [SerializeField] private Quaternion baseVal;
    [SerializeField] private Quaternion endVal;
    [SerializeField] private float speed;

    [Header("Rewards")]
    [SerializeField] private float timeToAdd;
    [SerializeField] private float timeToLose;
    [SerializeField] private bool alterTime;

    bool goingBack = false;
    [SerializeField]
    private Vector2 successRange;
    public override GameObject GetControllable(MiniGameController source)
    {
        return gameObject;
    }

    public override void Init(MiniGameController source, IMiniGameDependent objs)
    {
        this.source = source;
    }

    public void Interact()
    {
        result = 0;
        float angle = needle.transform.localRotation.eulerAngles.z;
        result = (angle < successRange.x || angle > successRange.y) ? 1 : 0;
        OnFinish();
    }

    public override void OnFinish()
    {
        source.OnFinishMiniGame(result);
        //TEMPORARY
        if (alterTime) {
            if ((InputManager.instance.isVirusWithBase(source.gameObject) && InputManager.instance.isVirusOnBody()) || (!InputManager.instance.isVirusWithBase(source.gameObject) && !InputManager.instance.isVirusOnBody()))
            {
                if (result == 1)
                {
                    GameStateManager.instance.AddSwitchTimer(timeToAdd);
                    SFXManager.MiniWinSFX(); // win sfx
                }
                else GameStateManager.instance.TakeSwitchTimer(timeToLose);
                SFXManager.MiniLoseSFX(); // lose sfx
            }
            else if((!InputManager.instance.isVirusWithBase(source.gameObject) && InputManager.instance.isVirusOnBody()) || (InputManager.instance.isVirusWithBase(source.gameObject) && !InputManager.instance.isVirusOnBody()))
            {
                if (result == 1)
                {
                    GameStateManager.instance.TakeSwitchTimer(timeToLose);
                    SFXManager.MiniWinSFX(); // win sfx
                }
                else GameStateManager.instance.AddSwitchTimer(timeToAdd);
                SFXManager.MiniLoseSFX(); // lose sfx
            }
        }
        Destroy(gameObject);
    }

    public override void Pause()
    {
    }

    public override void Resume(int result)
    {
    }

    private void Start()
    {
        Debug.Log(needle.transform.localRotation.ToString());
        SFXManager sfxManager = GameObject.FindWithTag("SFX")?.GetComponent<SFXManager>();
    }

    private void Update()
    {
        needle.transform.rotation = Quaternion.Lerp(needle.transform.localRotation, goingBack ? baseVal : endVal, speed * Time.deltaTime);
        if(Mathf.Abs(needle.transform.localRotation.z - (goingBack ? baseVal : endVal).z) < 0.05f) goingBack = !goingBack;

        if (Input.GetKeyDown(KeyCode.L))
        {
            Interact();
        }
    }
        
}
