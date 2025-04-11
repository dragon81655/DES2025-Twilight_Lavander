using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject lookRef;
    [SerializeField] private GameObject posRef;

    [SerializeField] private float stopOffSet;
    [SerializeField] private float speedRatio;

    [HideInInspector]
    public float _offset;
    [SerializeField]
    private float offset;

    Vector3 truePos = Vector3.zero;
    float m = 0f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _offset = offset;
    }

    public Vector3 GetTargetPos()
    {
        return truePos;
    }

    // Update is called once per frame
    void Update()
    {
        truePos = lookRef.transform.position + (posRef.transform.position - lookRef.transform.position) * _offset;
        if (Mathf.Abs(transform.position.x - lookRef.transform.position.x) > 0.05f || Mathf.Abs(transform.position.z - lookRef.transform.position.z) > 0.05f)
        transform.LookAt(lookRef.transform.position);
        Vector3 dis = truePos - transform.position;
        m = dis.magnitude;
        if (m < stopOffSet)
        {
            dis = Vector3.zero;
        }
        
        rb.velocity = dis*speedRatio;
    }
}
