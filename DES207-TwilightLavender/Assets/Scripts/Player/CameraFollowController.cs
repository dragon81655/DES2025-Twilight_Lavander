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

    [Header("Smoothing")]
    public float offset;
    [SerializeField] private float speedLerp;
    [SerializeField] private float lerpTimer;

    private float _offset;

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

    public void Set_OffSet(float offset)
    {
        _offset = offset;
    }

    public void SetOffSet(float offset)
    {
        this.offset = offset;
    }
    public void ChangeOffSet(float offset)
    {
        this.offset = Mathf.Clamp(this.offset + offset, 0.1f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _offset = Mathf.MoveTowards(_offset, offset, Time.deltaTime * speedLerp);

        truePos = lookRef.transform.position + (posRef.transform.position - lookRef.transform.position) * offset;
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
