using UnityEngine;

public class CameraRefController : MonoBehaviour
{
    [SerializeField] private GameObject lookAtRef;
    [SerializeField] private float maxOffSet;
    private float distance;

    [SerializeField] private float camSensitivity;
    private void Start()
    {
        distance = Vector3.Distance(transform.position, lookAtRef.transform.position);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = -Input.mousePositionDelta.x;
        float y = Input.mousePositionDelta.y;

        transform.LookAt(lookAtRef.transform.position);

        transform.position += transform.right * camSensitivity * x + transform.up * camSensitivity * y;
        if (Vector3.Distance(transform.position, lookAtRef.transform.position) > maxOffSet)
        {
            transform.position = CorrectionVector() + lookAtRef.transform.position;
        }

    }

    private Vector3 CorrectionVector()
    {
        Vector3 toReturn = (transform.position - lookAtRef.transform.position).normalized * distance;
        return toReturn;
    }
}
