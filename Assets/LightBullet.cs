using UnityEngine;

public class LightBullet : MonoBehaviour
{
    public float speed = 40f;
    public GameObject dissolveEffectPrefab;
    public float killAngle = 27f;
    public float sphereRadius = 0.5f;

    private Vector3 moveDirection;
    private GameObject ignoreRoot; // Ignored object (flashlight)

    public void SetIgnoreRoot(GameObject root)
    {
        ignoreRoot = root;

        // Ignore physical collisions between the bullet and all flashlight colliders.
        Collider[] bulletColliders = GetComponentsInChildren<Collider>();
        Collider[] flashlightColliders = root.GetComponentsInChildren<Collider>();

        foreach (Collider bc in bulletColliders)
            foreach (Collider fc in flashlightColliders)
                Physics.IgnoreCollision(bc, fc, true);
    }

    void Start()
    {
        moveDirection = transform.forward;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        int steps = 30;
        float stepDistance = moveDistance / steps;

        Debug.DrawRay(transform.position, moveDirection * 2f, Color.red);

        for (int i = 0; i < steps; i++)
        {
            if (Physics.SphereCast(transform.position, sphereRadius, moveDirection, out RaycastHit hit, stepDistance + 0.1f))
            {
                GameObject rootObj = hit.transform.root.gameObject;

                // Pass through flashlight and other bullets
                if (rootObj == ignoreRoot || rootObj == gameObject)
                {
                    transform.Translate(moveDirection * stepDistance, Space.World);
                    continue;
                }

                if (rootObj.CompareTag("NPC"))
                {
                    float angle = Vector3.Angle(moveDirection, -hit.normal);
                    Debug.Log("Hit angle: " + angle);

                    if (angle < killAngle)
                    {
                        // Direct hit -> kill
                        if (PlayerScore.Instance != null)
                            PlayerScore.Instance.AddScore(1);

                        if (dissolveEffectPrefab != null)
                        {
                            Vector3 direction = (rootObj.transform.position - transform.position).normalized;
                            GameObject effect = Instantiate(dissolveEffectPrefab, hit.transform.position, Quaternion.LookRotation(direction));
                            Destroy(effect, 3f);
                        }

                        Destroy(rootObj);
                        Destroy(gameObject);
                        return;
                    }
                    else
                    {
                        // Not direct hit -> bounce off
                        moveDirection = Vector3.Reflect(moveDirection, hit.normal).normalized;
                        transform.Translate(moveDirection * stepDistance, Space.World);
                        return;
                    }
                }

                // Obstacle -> bounce off
                moveDirection = Vector3.Reflect(moveDirection, hit.normal).normalized;
                transform.Translate(moveDirection * stepDistance, Space.World);
                return;
            }

            transform.Translate(moveDirection * stepDistance, Space.World);
        }

        Destroy(gameObject, 3f);
    }
}
