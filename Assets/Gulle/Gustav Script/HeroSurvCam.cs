using UnityEngine;

public class HeroSurvCam : MonoBehaviour
{
    public Transform target;  // Player to follow
    public float height = 10f;  // Height of the camera
    public float distance = 6f;  // Distance from the player
    public float smoothSpeed = 5f;  // How smoothly the camera follows
    public float leadStrength = 2f;  // How much the camera leads

    private Vector3 velocity = Vector3.zero;

    // Set fixed angles to simulate the locked camera
    public float cameraPitch = 45f;  // Fixed vertical rotation (locked at 45 degrees)
    private float yaw = 0f;  // Horizontal rotation based on player input

    private void Awake()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (!target) return;

        // Get the player's movement direction
        Rigidbody rb = target.GetComponent<Rigidbody>();
        Vector3 playerVelocity = rb != null ? rb.linearVelocity : Vector3.zero;

        Vector3 leadOffset = playerVelocity.normalized * leadStrength;

        // Desired camera position with a fixed angle
        Vector3 desiredPosition = target.position + leadOffset - transform.forward * distance + Vector3.up * height;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed * Time.deltaTime);

        // Lock the camera's rotation to the fixed vertical angle (cameraPitch)
        // The only thing that changes is the horizontal rotation (yaw) based on player movement
        transform.eulerAngles = new Vector3(cameraPitch, yaw, 0f);

        // Keep the camera facing the player
        transform.LookAt(target.position + Vector3.up * 2f);
    }

    public void RotateCamera(float mouseX)
    {
        // This method adjusts only the horizontal rotation (yaw)
        yaw += mouseX * 0.035f * 200f; // Adjust multiplier as needed
    }
}
