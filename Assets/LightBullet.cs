using UnityEngine;

public class LightBullet : MonoBehaviour
{
    public float speed = 40f;
    public GameObject dissolveEffectPrefab;
    public float bounceDetectRadius = 0.5f;
    public float killDetectRadius = 0.3f;

    private Vector3 moveDirection;

    void Start()
    {
        moveDirection = transform.forward;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;

        // Small SphereCast checks the direct hit first.
        if (Physics.SphereCast(transform.position, killDetectRadius, moveDirection, out RaycastHit killHit, moveDistance + 0.3f))
        {
            GameObject rootObj = killHit.transform.root.gameObject;

            if (rootObj.name == "Flashlight")
            {
                transform.Translate(moveDirection * moveDistance, Space.World);
                return;
            }

            if (rootObj.CompareTag("NPC"))
            {
                // Direct hit -> kill
                if (dissolveEffectPrefab != null)
                    Instantiate(dissolveEffectPrefab, killHit.transform.position, Quaternion.identity);
                Destroy(rootObj);
                Destroy(gameObject);
                return;
            }

            // Obstacle - bounce off
            moveDirection = Vector3.Reflect(moveDirection, killHit.normal).normalized;
            transform.Translate(moveDirection * moveDistance, Space.World);
            return;
        }

        // Big SphereCast checks the edge of the NPC for bouncing off
        if (Physics.SphereCast(transform.position, bounceDetectRadius, moveDirection, out RaycastHit bounceHit, moveDistance + 0.3f))
        {
            GameObject rootObj = bounceHit.transform.root.gameObject;

            if (rootObj.name == "Flashlight")
            {
                transform.Translate(moveDirection * moveDistance, Space.World);
                return;
            }

            if (rootObj.CompareTag("NPC"))
            {
                moveDirection = Vector3.Reflect(moveDirection, bounceHit.normal).normalized;
                transform.Translate(moveDirection * moveDistance, Space.World);
                return;
            }
        }

        transform.Translate(moveDirection * moveDistance, Space.World);
        Destroy(gameObject, 3f);
    }
}
