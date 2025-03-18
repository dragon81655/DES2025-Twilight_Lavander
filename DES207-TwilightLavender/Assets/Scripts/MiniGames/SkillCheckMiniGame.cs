using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SkillCheckMiniGame : BaseActivityController, IInteractorHandler
{
    private MiniGameController source;
    private int result = -1;

    [Header("Objects")]
    [SerializeField] private GameObject needle;

    [Header("Parameters")]
    [SerializeField] private Quaternion baseVal;
    [SerializeField] private Quaternion endVal;
    [SerializeField] private float speed;

    bool goingBack = false;
    private Vector2 successRange= Vector2.zero;
    public override GameObject GetControllable(MiniGameController source)
    {
        return gameObject;
    }

    public override void Init(MiniGameController source)
    {
        this.source = source;
        InputManager.instance.LockSwitch();

    }

    public void Interact()
    {
        result = 0;
        float angle = needle.transform.localRotation.eulerAngles.z / 180;
        result = (angle > successRange.x && angle < successRange.y) ? 1 : 0;
        OnFinish();
    }

    public override void OnFinish()
    {
        source.OnFinishMiniGame(result);
        InputManager.instance.UnlockSwitch();
        Destroy(gameObject);
    }

    public override void Pause()
    {
    }

    public override void Resume(int result)
    {
    }

    private void Update()
    {
        needle.transform.rotation = Quaternion.Lerp(needle.transform.localRotation, goingBack ? baseVal : endVal, speed * Time.deltaTime);
        if (needle.transform.localRotation.Equals(goingBack ? baseVal : endVal)) goingBack = !goingBack;
    }
        
}
