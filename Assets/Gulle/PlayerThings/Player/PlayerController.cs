using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    public float rotationSpeed = 10f;
    public Camera mainCamera;

    private Vector2 moveInput;
    private Rigidbody rb;
    private PlayerInput playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        
      
    }

    private void Update()
    {
        if (playerInput == null) return;
        moveInput = playerInput.actions["Move"]?.ReadValue<Vector2>() ?? Vector2.zero;
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotateTowardsMouse();
    }

    private void MovePlayer()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 targetVelocity = moveDirection * moveSpeed;
            Vector3 velocityDiff = targetVelocity - rb.linearVelocity;
            Vector3 force = velocityDiff * acceleration;

            rb.AddForce(force, ForceMode.Acceleration);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }
    }

    private void RotateTowardsMouse()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y;

            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
