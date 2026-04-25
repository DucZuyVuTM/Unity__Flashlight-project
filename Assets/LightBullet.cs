using UnityEngine;

public class LightBullet : MonoBehaviour
{
    public float speed = 20f;
    public GameObject dissolveEffectPrefab;

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, moveDistance + 0.5f))
        {
            Debug.Log("Hit: " + hit.collider.name + " | Tag: " + hit.collider.tag);

            if (hit.collider.CompareTag("NPC"))
            {
                if (dissolveEffectPrefab != null)
                    Instantiate(dissolveEffectPrefab, hit.transform.position, Quaternion.identity);

                Destroy(hit.collider.gameObject);
                Destroy(gameObject);
                return;
            }
        }

        transform.Translate(Vector3.forward * moveDistance);
        Destroy(gameObject, 3f);
    }
}
