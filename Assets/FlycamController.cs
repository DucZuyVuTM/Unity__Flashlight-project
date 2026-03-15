using UnityEngine;
using UnityEngine.InputSystem;

public class FlycamController : MonoBehaviour
{
    [Header("Cài đặt tốc độ")]
    public float moveSpeed = 15f;
    public float lookSpeed = 60f;
    public float acceleration = 5f;

    private Vector3 currentVelocity;
    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotationX = rot.x;
        rotationY = rot.y;
    }

    void Update()
    {
        // Lấy trạng thái bàn phím hiện tại
        var kb = Keyboard.current;
        if (kb == null) return;

        // --- Tính toán hướng phẳng ---
        Vector3 forward = transform.forward;
        forward.y = 0; // Triệt tiêu độ cao để luôn song song mặt đất
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0; // Đảm bảo khi di chuyển ngang không bị bay lên/xuống
        right.Normalize();

        // --- 1. DI CHUYỂN (U-I-O-J-K-L) ---
        Vector3 targetInput = Vector3.zero;

        if (kb.iKey.isPressed) targetInput += forward;     // Tiến
        if (kb.kKey.isPressed) targetInput -= forward;     // Lùi
        if (kb.jKey.isPressed) targetInput -= right;       // Trái
        if (kb.lKey.isPressed) targetInput += right;       // Phải
        if (kb.oKey.isPressed) targetInput += Vector3.up;  // Lên
        if (kb.uKey.isPressed) targetInput -= Vector3.up;  // Xuống

        currentVelocity = Vector3.Lerp(currentVelocity, targetInput.normalized * moveSpeed, acceleration * Time.deltaTime);
        transform.position += currentVelocity * Time.deltaTime;

        // --- 2. XOAY GÓC NHÌN (Numpad 8-5-4-6) ---
        if (kb.numpad8Key.isPressed) rotationX -= lookSpeed * Time.deltaTime;
        if (kb.numpad5Key.isPressed) rotationX += lookSpeed * Time.deltaTime;
        if (kb.numpad4Key.isPressed) rotationY -= lookSpeed * Time.deltaTime;
        if (kb.numpad6Key.isPressed) rotationY += lookSpeed * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
}
