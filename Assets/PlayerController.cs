using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;            
    public float jumpForce = 5f;        
    public float rotationSpeed = 100f;  

    private Rigidbody rb;               
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Đảm bảo đèn pin không bị xoay khi va chạm
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 1. Lấy input và tính toán hướng trong Update để mượt mà
        HandleInput();  
        HandleRotation();
        
        // 2. Nhảy (Nên check Grounded để tránh nhảy vô tận)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void FixedUpdate()
    {
        // 3. Thực hiện di chuyển vật lý trong FixedUpdate
        ApplyMovement();
    }

    void HandleInput()
    {
        if (Keyboard.current == null) return;

        float moveX = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();
        float moveZ = Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue();

        moveDirection = (transform.forward * moveZ) + (transform.right * moveX);
        moveDirection.y = 0;
    }

    void ApplyMovement()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            
            Vector3 targetVelocity = moveDirection.normalized * speed;

            // --- BẮT ĐẦU LOGIC CHỐNG XUYÊN TƯỜNG ---
            // Chúng ta bắn một hình cầu vô hình nhỏ hơn Đèn pin một chút về phía trước
            // 0.4f là bán kính (radius), 0.3f là khoảng cách quét (distance)
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.4f, moveDirection, out hit, 0.3f))
            {
                // Nếu vật chạm phải là Cube (hoặc bất kỳ thứ gì có Collider)
                // Chúng ta dừng vận tốc di chuyển lại
                targetVelocity = Vector3.zero;
            }
            // --- KẾT THÚC LOGIC CHỐNG XUYÊN TƯỜNG ---

            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void HandleRotation()
    {
        if (Keyboard.current == null) return;

        float rotateInput = Keyboard.current.rightArrowKey.ReadValue() - Keyboard.current.leftArrowKey.ReadValue();
        float rotationAmount = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotationAmount, Space.World);
    }
}
