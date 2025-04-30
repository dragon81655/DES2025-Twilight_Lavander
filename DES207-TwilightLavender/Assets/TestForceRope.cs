using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForceRope : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    float speed = 5f;
    Vector3 anchoredPos;
    bool anchored = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cv = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            cv += Vector3.right* speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cv += -Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            cv += Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cv += -Vector3.up * speed;
        }
        rb.velocity = cv;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.isKinematic = !rb.isKinematic;

            foreach(Transform t in transform.parent)
            {
                t.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                t.GetComponent<Rigidbody2D>().angularVelocity = 0;
            }
        }
        
    }
    private void LateUpdate()
    {
        if (anchored)
        {
        }
    }
}
