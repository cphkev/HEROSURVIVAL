using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public Camera mainCamera;

    private Vector2 moveInput;
    private Rigidbody rb;
    private PlayerInput playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smooth movement
    }

    private void Update()
    {
        if (playerInput == null) return; 
        moveInput = playerInput.actions["Move"]?.ReadValue<Vector2>() ?? Vector2.zero;
        RotateTowardsMouse();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        rb.linearVelocity = moveDirection * moveSpeed; // Use velocity instead of MovePosition
    }

    private void RotateTowardsMouse()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetPosition = hit.point;
            targetPosition.y = transform.position.y; // Keep player on the same Y-axis
            transform.LookAt(targetPosition);
        }
    }
}
