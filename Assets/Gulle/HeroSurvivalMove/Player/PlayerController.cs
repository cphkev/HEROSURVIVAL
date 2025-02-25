using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust movement speed
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
        
        RotateTowardsMouse();
    }

    
    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 targetPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(targetPosition);
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
