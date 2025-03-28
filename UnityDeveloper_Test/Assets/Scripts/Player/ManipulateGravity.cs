using UnityEngine;

public class ManipulateGravity : MonoBehaviour
{
    private const string UPPERARROWKEY = "upArrow";
    private const string DOWNARROWKEY = "downArrow";
    private const string LEFTARROWKEY = "leftArrow";
    private const string RIGHTARROWKEY = "rightArrow";
    [SerializeField] private float gravityStrength = 9.81f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 gravityDirection = Vector3.down; // Default gravity
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        InputManager.Instance.OnJump += InputManager_OnJump;
        InputManager.Instance.OnManipulateGravity += InputManager_OnManipulateGravity;
    }

    private void InputManager_OnManipulateGravity(object sender, InputManager.OnHologramEventArgs e)
    {
        switch (e.keyName)
        {
            case UPPERARROWKEY:
                SetGravity(transform.forward);
                break;
            case DOWNARROWKEY:
                SetGravity(-transform.forward);
                break;
            case LEFTARROWKEY:
                SetGravity(-transform.right);
                break;
            case RIGHTARROWKEY:
                SetGravity(transform.right);
                break;
        }
    }

    private void InputManager_OnJump(object sender, System.EventArgs e)
    {
        Jump();
    }

    void Update()
    {
        AlignToGravity();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public Vector3 GetGravityDirection()
    {
        return gravityDirection;
    }


    void ApplyGravity()
    {
        velocity += gravityDirection * gravityStrength * Time.deltaTime;
        rb.velocity = velocity;
    }

    void Jump()
    {
        velocity = -gravityDirection * jumpForce;
    }

    public void SetGravity(Vector3 newGravityDirection)
    {
        gravityDirection = newGravityDirection.normalized;
        velocity = Vector3.zero;
    }

    void AlignToGravity()
    {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 7f);
    }
}