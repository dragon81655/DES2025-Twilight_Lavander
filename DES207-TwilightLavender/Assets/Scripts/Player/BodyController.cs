using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour, IAxisHandler, IInteractorHandler
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Camera cam;
    
    private Rigidbody rb;
    private Vector3 dir;
    public void Interact()
    {
        Transform t = Camera.main.transform;
        RaycastHit hit;
        if (Physics.Raycast(t.position, t.forward, out hit, 4))
        {
            IInteractable i = hit.transform.GetComponent<IInteractable>();
            if (i != null)
            {
                i.Interact(gameObject);
            }
        }
    }

    public void Move(float x, float y)
    {
        Vector3 lookAt = Vector3.zero;
        if (y != 0)
            lookAt += new Vector3(cam.transform.forward.x * y, 0, cam.transform.forward.z * y);
        
        if (x != 0)
            lookAt += new Vector3(cam.transform.right.x * x, 0, cam.transform.right.z * x);

        x = Mathf.Abs(x);
        y = Mathf.Abs(y);
        float speedMult = 0;
        if (x != 0 && y != 0)
            speedMult = new Vector2(x,y).magnitude;
        else speedMult = x == 0 ? y : x;

        transform.LookAt(lookAt + transform.position);
        dir = transform.forward * speedMult ;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }
   
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = dir * playerSpeed + new Vector3(0, rb.velocity.y, 0);
    }
}
