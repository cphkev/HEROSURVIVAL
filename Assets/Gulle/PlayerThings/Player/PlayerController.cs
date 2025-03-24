using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    public float rotationSpeed = 10f;

    public float stepHeight = 0.5f;
    public float stepSmooth = 0.1f;

   
    
    private Vector3 velocity;
    public Camera mainCamera;
    public Animator animator; 

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

    // Get input from the player (e.g., WASD or left stick)
    Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

    // Calculate the movement direction in world space
    Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

    // Update the MoveX and MoveY parameters for the animator (for blend tree)
    animator.SetFloat("MoveX", moveInput.x);
    animator.SetFloat("MoveY", moveInput.y);

    // If there is input, calculate whether we're backpedaling or running
    if (moveDirection.magnitude > 0.1f)
    {
        // Calculate the direction to the mouse
        Vector3 targetPosition = GetMouseWorldPosition();
        Vector3 toMouseDirection = (targetPosition - transform.position).normalized;

        // Check if the player is moving forward or backward relative to the mouse
        float dotProduct = Vector3.Dot(moveDirection, toMouseDirection);

        // If the dot product is positive, we're moving towards the mouse (run)
        // If the dot product is negative, we're moving away from the mouse (backpedal)
        if (dotProduct > 0)
        {
            // Moving toward the mouse: run animation
            animator.SetBool("IsBackpedaling", false);
        }
        else if (dotProduct < 0)
        {
            // Moving away from the mouse: backpedal animation
            animator.SetBool("IsBackpedaling", true);
        }
    }
    else
    {
        // If no movement input, reset backpedaling
        animator.SetBool("IsBackpedaling", false);
    }
}
    



    private void FixedUpdate()
    {
        MovePlayer();
        RotateTowardsMouse();
        HandleStepClimb();

    }

    private Vector3 GetMouseWorldPosition()
{
    if (mainCamera == null) return Vector3.zero;

    Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
    if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
    {
        return hit.point; // Mouse position in world space
    }

    return Vector3.zero;
}

   private void MovePlayer()
{
    // Get input from the player (e.g., WASD or left stick)
    Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

    // Calculate move direction based on input (WASD or left stick)
    Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

    if (moveDirection.magnitude > 0.1f)
    {
        // Apply movement directly to the rigidbody's velocity (smooth movement)
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
    }
    else
    {
        // Decelerate smoothly to stop the player when no input is detected
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0); // Keep the Y axis velocity intact for gravity
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

    private void HandleStepClimb()
    {
     RaycastHit hitLower;
    RaycastHit hitUpper;

    Vector3 origin = transform.position + Vector3.up * 0.1f;
    Vector3 upperOrigin = transform.position + Vector3.up * stepHeight;

    if (Physics.Raycast(origin, transform.forward, out hitLower, 0.5f) &&
        !Physics.Raycast(upperOrigin, transform.forward, out hitUpper, 0.5f))
    {
        transform.position += Vector3.up * stepSmooth;
    }

    }

  

}
