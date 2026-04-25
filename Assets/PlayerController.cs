using UnityEngine;
using UnityEngine.InputSystem;  // It is required for using Keyboard.current.

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;            // Movement speed (m/s)
    public float jumpForce = 5f;        // Jump force (N·s)
    public float rotationSpeed = 100f;  // Rotation speed (°/s)

    private Rigidbody rb;               // Rigidbody component

    void Start()
    {
        // Get the Rigidbody component on the object at the start.
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();  // Create every movement
        HandleRotation();  // Create every rotation
        HandleJump();      // Create every jump
    }

    void HandleMovement()
    {
        // Check if the keyboard is connected.
        if (Keyboard.current == null) return;

        // Reads values from keys: Returns 1 if pressed, 0 if released.
        float moveX = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(); // A/D for X
        float moveZ = Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue(); // W/S for Z

        // Create a vector representing the direction of movement
        // (X represents horizontal, Z represents vertical/forward/backward)
        Vector3 moveDirection = (transform.forward * moveZ) + (transform.right * moveX);
        moveDirection.y = 0;

        // Move objects in real time.
        transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
    }

    void HandleRotation()
    {
        if (Keyboard.current == null) return;

        // Read the arrow keys: Right returns 1, Left returns -1
        float rotateInput = Keyboard.current.rightArrowKey.ReadValue()
            - Keyboard.current.leftArrowKey.ReadValue();

        // Calculate real-time rotation angle
        float rotationAmount = rotateInput * rotationSpeed * Time.deltaTime;

        // Rotate around the world's Y-axis to ensure accurate horizontal rotation
        transform.Rotate(Vector3.up, rotationAmount, Space.World);
    }

    void HandleJump()
    {
        // If press the Space key and keyboard activated
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Apply an instantaneous upward force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
