using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject lookRef;
    [SerializeField] private GameObject posRef;

    [SerializeField] private float stopOffSet;
    [SerializeField] private float tpOffSet;
    [SerializeField] private float speedRatio;

    Vector3 truePos = Vector3.zero;
    float m = 0f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         truePos = (posRef.transform.position + lookRef.transform.position) / 2;
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

    private void OnCollisionStay(Collision collision)
    {
        if (m > tpOffSet)
        {
            Debug.Log("Correction!");
            transform.position = truePos;
        }
    }
}
