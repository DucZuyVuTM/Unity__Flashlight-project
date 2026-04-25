using UnityEngine;

public class SpiralSpawner : MonoBehaviour
{
    public GameObject flashlightPrefab;   // Model .fbx
    public int numOfObj = 20;             // Object amount
    public float radiusIncrement = 0.5f;  // Expansion of the vortex
    public float angleIncrement = 20f;    // Twist of the ring

    void Start()
    {
        SpawnSpiral(); // Start spawning right after start
    }

    void SpawnSpiral()
    {
        for (int i = 0; i < numOfObj; i++)
        {
            float angle = i * angleIncrement;
            float radius = i * radiusIncrement;

            Vector3 relativePos = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                1.0f,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            GameObject flashlight = Instantiate(flashlightPrefab, transform.position + relativePos, Quaternion.identity, transform);

            flashlight.transform.localScale = Vector3.one;

            Renderer[] rends = flashlight.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rends)
            {
                r.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}
