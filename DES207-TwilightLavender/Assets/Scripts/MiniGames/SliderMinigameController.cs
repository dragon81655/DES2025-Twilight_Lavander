using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class SliderMinigameController : BaseActivityController, IAxisHandler
{
    [Header("Sliders config")]
    [SerializeField] private float offset;

    [SerializeField] private float limitLeft;
    [SerializeField] private float limitRight;

    [SerializeField] private float sliderSpeed;

    [Header("Sliders")]
    [SerializeField] private List<RectTransform> sliders;
    [SerializeField] private List<LevelConfig> configs;
    private LevelConfig config;
    private IMiniGameDependent toNotify;
    [Header("Level")]
    [SerializeField] private RawImage background;

    private bool canMove = false;
    private int currentlySelected = 0;

    private MiniGameController source;

    public override GameObject GetControllable(MiniGameController source)
    {
        return gameObject;
    }

    public override void Init(MiniGameController source, IMiniGameDependent objs)
    {
        this.source = source;
        config = configs[UnityEngine.Random.Range(0, configs.Count)];
        background.texture = config.levelTexture;
        toNotify = objs;
        InputManager.instance.LockSwitch();

    }

    public void Move(float x, float y)
    {
        float moveDir = x * sliderSpeed;

        if(Mathf.Abs(y) > 0.1f && canMove)
        {
            currentlySelected = Mathf.Clamp((y > 0 ? 1 : -1) + currentlySelected, 0, sliders.Count - 1);
            canMove = false;
        }
        if (y == 0) canMove = true;
        MoveSlider(moveDir);
    }

    private void MoveSlider(float moveDir)
    {
        //Debug.Log(moveDir);
        if (moveDir == 0) return;
        RectTransform t = sliders[currentlySelected];
        if (moveDir < 0 && t.anchoredPosition3D.x <= limitLeft)
            return;
        if (moveDir > 0 && t.anchoredPosition3D.x >= limitRight)
            return;
        t.localPosition += new Vector3(moveDir * Time.deltaTime, 0, 0);
        CheckFinish();
    }

    private void CheckFinish()
    {
        for(int i = 0; i < sliders.Count; i++)
        {
            if (!(sliders[i].anchoredPosition3D.x > config.slide[i] - offset && sliders[i].anchoredPosition3D.x < config.slide[i] + offset)) return;
        }
        OnFinish();
    }

    public override void OnFinish()
    {
        source.OnFinishMiniGame(0);
        if(toNotify!= null)
        {
            toNotify.Notify(1);
        }
        InputManager.instance.UnlockSwitch();
        Destroy(gameObject);
    }

    public override void Pause()
    {
        
    }

    public override void Resume(int result)
    {
        
    }

    void Update()
    {

    }


    [Serializable]
    private struct LevelConfig
    {
        public List<float> slide;
        public Texture levelTexture; 
    }
}
