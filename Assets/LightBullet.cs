using UnityEngine;

public class LightBullet : MonoBehaviour
{
    public float speed = 40f;
    public GameObject dissolveEffectPrefab;
    public float killAngle = 27f;
    public float sphereRadius = 0.5f;

    private Vector3 moveDirection;
    private GameObject ignoreRoot; // Ignored object (flashlight)
    private bool isDead = false;

    public Vector3 GetMoveDirection() => moveDirection;

    public void SetIgnoreRoot(GameObject root)
    {
        ignoreRoot = root;

        // Ignore physical collisions between the bullet and all flashlight colliders
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
        if (isDead) return;

        float moveDistance = speed * Time.deltaTime;
        int steps = 50;
        float stepDistance = moveDistance / steps;

        Debug.DrawRay(transform.position, moveDirection * 2f, Color.red);

        for (int i = 0; i < steps; i++)
        {
            // Use OverlapSphere instead of SphereCast to avoid missing any hits.
            Collider[] hits = Physics.OverlapSphere(transform.position + moveDirection * stepDistance, sphereRadius);
            
            bool handled = false;
            foreach (Collider col in hits)
            {
                GameObject rootObj = col.transform.root.gameObject;
                if (rootObj == ignoreRoot || rootObj == gameObject) continue;

                if (rootObj.CompareTag("NPC"))
                {
                    // SphereCast is used to get normal
                    if (Physics.SphereCast(transform.position, sphereRadius, moveDirection, out RaycastHit hit, stepDistance * 2f))
                    {
                        if (hit.transform.root.gameObject == rootObj)
                        {
                            float angle = Vector3.Angle(moveDirection, -hit.normal);
                            Debug.Log("NPC Hit Angle: " + angle);

                            if (angle < killAngle)
                            {
                                isDead = true;
                                if (PlayerScore.Instance != null) PlayerScore.Instance.AddScore(1);
                                if (dissolveEffectPrefab != null)
                                {
                                    Vector3 dir = (rootObj.transform.position - transform.position).normalized;
                                    Destroy(Instantiate(dissolveEffectPrefab, transform.position, Quaternion.LookRotation(dir)), 3f);
                                }
                                Destroy(rootObj);
                                Destroy(gameObject);
                                return;
                            }
                            else
                            {
                                moveDirection = Vector3.Reflect(moveDirection, hit.normal).normalized;
                                handled = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (Physics.SphereCast(transform.position, sphereRadius, moveDirection, out RaycastHit hit, stepDistance * 2f))
                    {
                        moveDirection = Vector3.Reflect(moveDirection, hit.normal).normalized;
                        handled = true;
                        break;
                    }
                }
            }

            if (handled)
            {
                transform.Translate(moveDirection * stepDistance, Space.World);
                return;
            }

            transform.Translate(moveDirection * stepDistance, Space.World);
        }

        Destroy(gameObject, 3f);
    }
}
