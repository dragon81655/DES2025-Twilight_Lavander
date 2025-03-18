using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour, ICamAxisHandler, IInputChangeSummoner
{
    [SerializeField] private float cameraSensibility;
    [SerializeField] private float maxOffSet;
    [SerializeField] private GameObject cameraRef;
    [SerializeField] private GameObject cameraLookRef;
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
    }

    private Vector3 CorrectionVector()
    {
        Vector3 toReturn = (cameraRef.transform.position - cameraLookRef.transform.position).normalized * distance;
        return toReturn;
    }

    public void Notify()
    {
        if (t)
            cameraSensibility /= 4;
        else
            cameraSensibility *= 4;
        t = !t;
    }
}
