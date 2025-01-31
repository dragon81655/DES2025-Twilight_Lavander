using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    [Header("Header")]
    [SerializeField]
    private float sensivity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles + new Vector3(0, sensivity * mouseX, 0));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(sensivity * -mouseY, 0, 0));
    }
}
