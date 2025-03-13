using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField] private float speed;
    private Camera cam;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    Vector3 lockedRight= Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("VerticalKB");
        float y = Input.GetAxis("HorizontalKB");

        if (x != 0 || y != 0)
        {
            Vector3 lookAt = Vector3.zero;
            if(x != 0)
            {
                lookAt += new Vector3(cam.transform.forward.x *x, 0, cam.transform.forward.z*x);
                lockedRight = Vector3.zero;
            }
            if (y != 0)
            {
                lookAt += new Vector3(cam.transform.right.x * y, 0, cam.transform.right.z * y);
            }
            x = Mathf.Abs(x);
            y = Mathf.Abs(y);
            float speedMult = 0;
            if (x != 0 && y != 0)
                speedMult = x + y / 2;
            else speedMult = x == 0 ? y : x;
            transform.LookAt(lookAt + transform.position);
            Vector3 toAdd = transform.forward * speed * speedMult + new Vector3(0, rb.velocity.y, 0);
            rb.velocity = toAdd;
        }
    }
}
