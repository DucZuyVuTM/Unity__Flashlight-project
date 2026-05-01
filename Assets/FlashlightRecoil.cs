using UnityEngine;

public class FlashlightRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilDistance = 1f;
    public float recoilSpeed = 15f;
    public float returnSpeed = 8f;

    private Vector3 originalLocalPosition;
    private bool isReturning = false;

    void Start()
    {
        // Save the initial local position of the child prefab
        originalLocalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isReturning)
        {
            // Return to the original position
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalLocalPosition,
                returnSpeed * Time.deltaTime
            );

            // Returned -> stop
            if (Vector3.Distance(transform.localPosition, originalLocalPosition) < 0.001f)
            {
                transform.localPosition = originalLocalPosition;
                isReturning = false;
            }
        }
    }

    public void TriggerRecoil()
    {
        // Move backward according to local space
        transform.localPosition = originalLocalPosition - Vector3.forward * recoilDistance;
        isReturning = true;
    }
}
