using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracollider : MonoBehaviour
{

    public GameObject Camera;
    public GameObject Player;
    public float startDistance;
    public float currentDistance;
     void Start()
    {
        
         startDistance = Vector3.Distance(Camera.transform.position, Player.transform.position);

    }

    private void Update()
    {
         currentDistance = Vector3.Distance(Camera.transform.position, Player.transform.position);



        //if (startDistance != currentDistance)
        //{
        //    currentDistance = startDistance;
        //}
    }



    //private void OnCollisionEnter(Collision collision)
    //{
    //    currentDistance = startDistance;
    //}


    //private Vector3 startPosition;
    //private void Start()
    //{
    //    startPosition = gameObject.transform.position;


    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    transform.position = startPosition;
    //}
}
