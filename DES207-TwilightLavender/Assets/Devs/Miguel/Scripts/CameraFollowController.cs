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
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - lookRef.transform.position.x) > 0.05f || Mathf.Abs(transform.position.z - lookRef.transform.position.z) > 0.05f)
        transform.LookAt(lookRef.transform.position);
        Vector3 dis = posRef.transform.position - transform.position;
        float m = dis.magnitude;
        if (m < stopOffSet)
        {
            dis = Vector3.zero;
        }
        if(m > tpOffSet) transform.position = posRef.transform.position;
        rb.velocity = dis*speedRatio;
    }
}
