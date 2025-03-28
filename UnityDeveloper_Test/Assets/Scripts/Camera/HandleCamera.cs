using UnityEngine;

public class HandleCamera : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;

    private float xAxisRotation = 0f;  // Up-Down (Pitch)

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate only the camera (Up-Down)
        xAxisRotation -= mouseY;
        xAxisRotation = Mathf.Clamp(xAxisRotation, -90f, 90f); // Prevent flipping

        transform.localRotation = Quaternion.Euler(xAxisRotation, 0f, 0f);

        // Rotate only the camera (Left-Right)
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
