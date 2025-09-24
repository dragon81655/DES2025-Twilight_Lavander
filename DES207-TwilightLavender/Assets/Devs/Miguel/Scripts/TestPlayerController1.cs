using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController1 : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 currentMovement;
    [SerializeField]
    private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(currentMovement.x, 0, currentMovement.y) * Time.deltaTime * speed;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        currentMovement = ctx.ReadValue<Vector2>();
    }
}
