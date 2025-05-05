using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour, ICamAxisHandler, IInputChangeSummoner, IScrollable, ICamLockable
{
    [SerializeField] private float cameraSensibility;
    [SerializeField] private float maxOffSet;
    [SerializeField] private float rayCastCheckTimer;
    private float _rayCastCheckTimer;
    [SerializeField] private GameObject cameraRef;
    [SerializeField] private GameObject cameraLookRef;
    [SerializeField] private CameraFollowController cam;
    Vector2 v = Vector2.zero;
    [SerializeField] private float lerpSpeed;

    private float distance;
    private bool camLock = false;

    bool isController = true;

    public void MoveCam(float x, float y)
    {
        if (!isController)
        {
            v = new Vector2(-x, y);
            return;
        }

        if (!camLock)
        {
            v = new Vector2(-x, y) * 3;
        }
        else
        {
            cam.ChangeOffSet(y * Time.deltaTime * lerpSpeed/2);
        }
    }
    private void Start()
    {
        distance = Vector3.Distance(cameraLookRef.transform.position, cameraRef.transform.position);
        _rayCastCheckTimer = rayCastCheckTimer;
    }
    void Update()
    {
        cameraRef.transform.LookAt(cameraLookRef.transform.position);
        Vector3 t = (cameraRef.transform.up * v.y + cameraRef.transform.right * v.x) * cameraSensibility * Time.deltaTime;
        cameraRef.transform.position += t;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Vector3.Distance(cameraLookRef.transform.position, cameraRef.transform.position) - distance) > maxOffSet)
        {
            cameraRef.transform.position = CorrectionVector() + cameraLookRef.transform.position;
        }
        _rayCastCheckTimer -= Time.fixedDeltaTime;
        /*if(_rayCastCheckTimer <= 0 )
        {
            RaycastHit hit;
            if(Physics.Raycast(cameraLookRef.transform.position, -cam.transform.forward, out hit))
            {
                if(hit.transform != cam.transform && hit.transform != transform)
                {
                    Debug.Log("Adjust! " + hit.transform.name);
                    cam.transform.position = hit.transform.position + cam.transform.forward * 0.5f;

                    Vector3 t = cameraRef.transform.position - cameraLookRef.transform.position;
                    cam.SetOffSet(Vector3.Dot(cam.transform.position - cameraLookRef.transform.position, t)/t.sqrMagnitude);
                }
            }
            _rayCastCheckTimer = rayCastCheckTimer;
        }*/
    }

    private Vector3 CorrectionVector()
    {
        Vector3 toReturn = (cameraRef.transform.position - cameraLookRef.transform.position).normalized * distance;
        return toReturn;
    }

    public void Scroll(float val)
    {
        if (camLock && !isController)
        {
            cam.ChangeOffSet(val * Time.deltaTime * lerpSpeed);
        }
    }

    public void CamLock()
    {
        camLock = !camLock;
        Debug.Log(camLock);
    }

    public void Notify()
    {
        isController = "KB" != InputManager.instance.GetInputType(gameObject);
        Debug.Log("Notify switch " +isController);
    }
}
