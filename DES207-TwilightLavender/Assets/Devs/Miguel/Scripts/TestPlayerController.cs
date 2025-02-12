using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [Header("Movement Params")]
    [SerializeField] private float speedV;
    [SerializeField] private float speedH;

    [SerializeField] private float jumpForce;

    [SerializeField] private bool onGround = false;

    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = transform.forward * vertical * speedV + transform.right * horizontal * speedH + new Vector3(0, rb.velocity.y, 0);

        if (Input.GetButtonDown("Jump"))
        {
            if (Mathf.Abs(rb.velocity.y) < 0.1f)
            {
                if (onGround)
                {
                    rb.velocity = rb.velocity + transform.up * jumpForce;
                    onGround = false;
                }
            }
        }
        if (Input.GetButtonDown("Interact"))
        {
            Transform t = Camera.main.transform;
            RaycastHit hit;
            if(Physics.Raycast(t.position, t.forward, out hit, 4))
            {
                ITestInteractable i = hit.transform.GetComponent<ITestInteractable>();
                Debug.Log(i);
                if(i != null)
                {
                    i.Interact(gameObject);
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (onGround) return;
        if (collision.gameObject.layer == 3)
        {
            foreach (ContactPoint c in collision.contacts)
            {
                float dot = Vector3.Dot(transform.up, c.normal.normalized);
                if (dot > 0.5f)
                {
                    onGround = true;
                }
            }
        }
    }
}
