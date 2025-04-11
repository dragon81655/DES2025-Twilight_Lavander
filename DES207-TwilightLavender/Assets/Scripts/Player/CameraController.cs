using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour, ICamAxisHandler, IInputChangeSummoner
{
    [SerializeField] private float cameraSensibility;
    [SerializeField] private float maxOffSet;
    [SerializeField] private float rayCastCheckTimer;
    private float _rayCastCheckTimer;
    [SerializeField] private GameObject cameraRef;
    [SerializeField] private GameObject cameraLookRef;
    [SerializeField] private GameObject cam;
    Vector2 v = Vector2.zero;

    private float distance;

    bool t = true;
    public void MoveCam(float x, float y)
    {
        v = new Vector2(-x, y);
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
        if(_rayCastCheckTimer <= 0 )
        {
            RaycastHit hit;
            if(Physics.Raycast(cameraLookRef.transform.position, -cam.transform.forward, out hit, distance,LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore))
            {
                if(hit.transform != transform)
                {
                    Debug.Log("Adjust! " + hit.transform.name);
                    cam.transform.position = hit.transform.position + cam.transform.forward * 0.5f;
                }
            }
            _rayCastCheckTimer = rayCastCheckTimer;
        }
    }

    private Vector3 CorrectionVector()
    {
        //Debug.Log("Correction needed!");
        Vector3 toReturn = (cameraRef.transform.position - cameraLookRef.transform.position).normalized * distance;
        return toReturn;
    }

    public void Notify()
    {
    }
}
