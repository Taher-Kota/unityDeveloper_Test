using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 4f;
    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 inputVector = InputManager.Instance.GetNormalizedVector2Movement();
        moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y;

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

    public bool IsRunning()
    {
        return moveDirection.magnitude > 0.1f;
    }

}
