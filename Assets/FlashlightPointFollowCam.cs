using UnityEngine;

public class FlashlightPointFollowCam : MonoBehaviour
{
    [Header("Points in Flashlight")]
    public Transform BackPoint;
    public Transform frontPoint;

    [Header("Location settings")]
    public float distanceBack = 5f;
    public float heightOffset = 2f;
    public float pitchOffset = 10f;

    [Header("Smoothness")]
    public float followSpeed = 8f;
    public float lookSpeed = 12f;

    void LateUpdate()
    {
        if (BackPoint == null || frontPoint == null) return;

        // 1. Calculate the flashlight direction (Vector pointing from back to front)
        Vector3 flashlightDirection = (frontPoint.position - BackPoint.position).normalized;

        // 2. Find the target location for the camera
        // This spot is located behind the back point based on the lighting direction and is slightly higher.
        Vector3 targetPosition = BackPoint.position 
                                 - (flashlightDirection * distanceBack) 
                                 + (Vector3.up * heightOffset);

        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // 3. Calculate the angle of view
        // The camera will always be facing the front point
        Quaternion targetRotation = Quaternion.LookRotation(flashlightDirection, Vector3.up);
        
        // Multiply the quaternion by rotating it around the X-axis to point downwards.
        targetRotation *= Quaternion.Euler(pitchOffset, 0, 0);
        
        // Smooth camera rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }
}