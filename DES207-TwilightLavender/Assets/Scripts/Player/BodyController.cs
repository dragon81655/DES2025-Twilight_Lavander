using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour, IAxisHandler, IHiveMindSummoner, IInputChangeSummoner
{
    public float playerSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject model;
    
    private Rigidbody rb;
    private Vector3 dir;
   
    private bool usingHiveMind = false;
    public void StopPlayer()
    {
        dir = Vector3.zero;
    }
    public void Move(float x, float y)
    {
        if(usingHiveMind) return;
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

        model.transform.LookAt(lookAt + model.transform.position);
        dir = model.transform.forward * speedMult ;
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

    public void Summon()
    {
        usingHiveMind = !usingHiveMind;
        StopPlayer();
    }

    public void Notify()
    {
        usingHiveMind = false;
    }
}
