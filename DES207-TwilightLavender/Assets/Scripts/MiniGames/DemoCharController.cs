using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCharController : MonoBehaviour, IAxisHandler
{
    [SerializeField] private float speed;
    [SerializeField] private RectTransform target;
    [SerializeField] private DemoMultiPlayerMinigame gameController;
    [SerializeField] private int supposedResult;
    public void Move(float x, float y)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed;
    }

    public void Update()
    {
        if(transform.localPosition.x > target.localPosition.x - target.rect.width/2 && transform.localPosition.x < target.localPosition.x + target.rect.width / 2)
        {
            if (transform.localPosition.y > target.localPosition.y - target.rect.height / 2 && transform.localPosition.y < target.localPosition.y + target.rect.height / 2)
            {
                gameController.SetResult(supposedResult);
                gameController.OnFinish();
                
            }
        }
    }

    private void Start()
    {
        gameController = transform.parent.GetComponent<DemoMultiPlayerMinigame>();
    }
}
