using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CableMiniGame : BaseActivityController, ICamAxisHandler
{
    private MiniGameController source;
    private IMiniGameDependent toNotify;

    [SerializeField]
    private List<Rigidbody2D> cableEnding;
    private int currentlySelected = 0;

    [SerializeField]
    private Vector2 offSet;
    [SerializeField]
    private List<Vector3> lockPositions;

    private Vector4 lockPoint;

    float speed = 10.0f;

    private Vector3 currentPos;
    [SerializeField]
    private GameObject point;

    [SerializeField]
    private Vector3 vel;
    public override GameObject GetControllable(MiniGameController source)
    {
        return gameObject;
    }

    private void Start()
    {
        Init(null, null);
    }

    private bool IsKB()
    {
        return false; //InputManager.instance.GetInputType(gameObject) == "KB";
    }

    public override void Init(MiniGameController source, IMiniGameDependent objs)
    {
        this.source = source;
        toNotify = objs;
        currentPos = cableEnding[currentlySelected].transform.position;
        lockPoint = new Vector4(lockPositions[currentlySelected].x - offSet.x, lockPositions[currentlySelected].x + offSet.x,
            lockPositions[currentlySelected].y - offSet.y, lockPositions[currentlySelected].y + offSet.y);
    }

    public void MoveCam(float x, float y)
    {
        if (currentlySelected >= cableEnding.Count) return;
        if (Mathf.Abs(x) > 0.05f || Mathf.Abs(y) > 0.05f)
        {
            if (!cableEnding[currentlySelected].isKinematic)
                cableEnding[currentlySelected].isKinematic = true;
            Vector3 target = cableEnding[currentlySelected].transform.position;
            Vector3 mouse = GetCursor(x, -y);
            Vector3 moveDir = (mouse - target);
            cableEnding[currentlySelected].velocity = moveDir * speed;
            if (OnPlace())
            {
                NextCable();
            }
        }
        else
        {
            StopCable();
        }
    }
    private Vector3 GetCursor(float x, float y)
    {
        float t = 1;
        if (IsKB())
        {
            y = -y;
            t = 0.4f;
        }
        currentPos += new Vector3(x, y, 0) * speed * Time.deltaTime * t;
        point.transform.position = currentPos;
        return currentPos;

    }
    private bool OnPlace()
    {
        Vector3 pos = cableEnding[currentlySelected].transform.position;
        if (pos.x >= lockPoint.x && pos.x <= lockPoint.y)
        {
            if (pos.y >= lockPoint.z && pos.y <= lockPoint.w)
            {
                return true;
            }
        }
        return false;
    }

    private void NextCable()
    {
        StopCable();
        currentlySelected++;
        if (currentlySelected >= cableEnding.Count)
        {
            OnFinish();
            return;
        }
        lockPoint = new Vector4(lockPositions[currentlySelected].x - offSet.x, lockPositions[currentlySelected].x + offSet.x,
            lockPositions[currentlySelected].y - offSet.y, lockPositions[currentlySelected].y + offSet.y);
        currentPos = cableEnding[currentlySelected].transform.position;
        StartCable();
    }
    private void StopCable()
    {
        cableEnding[currentlySelected].isKinematic = true;
        cableEnding[currentlySelected].angularVelocity = 0;
        cableEnding[currentlySelected].velocity = Vector3.zero;
    }
    private void StartCable()
    {
        cableEnding[currentlySelected].isKinematic = false;
    }
    public override void OnFinish()
    {
        Destroy(gameObject);
    }

    public override void Pause()
    {
    }

    public override void Resume(int result)
    {
    }
}
