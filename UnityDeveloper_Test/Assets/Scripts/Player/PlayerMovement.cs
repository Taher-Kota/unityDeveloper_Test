using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 4f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 inputVector = InputManager.Instance.GetNormalizedVector2Movement();
        Vector3 moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y;

        if (moveDirection.magnitude > .1f)
        {
            RotatePlayer(moveDirection);
        }

        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
    }

    void RotatePlayer(Vector3 moveDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
