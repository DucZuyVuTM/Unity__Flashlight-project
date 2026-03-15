using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject flashlightPrefab; // Model .fbx
    public float spawnInterval = 2f; // Spawn every 2 seconds

    void Start()
    {
        // Call 'SpawnObject', start in 1s, and repeat every 'spawnInterval' seconds
        InvokeRepeating(nameof(SpawnObject), 1f, spawnInterval);
    }

    void SpawnObject()
    {
        // Create random position (from 0 to 3) in X, Y, Z axis
        Vector3 randomPos = new Vector3(Random.value, Random.value, Random.value);
        
        // Spawn object in that random position
        GameObject flashlight = Instantiate(
            flashlightPrefab, randomPos, Quaternion.identity, transform);

        // Renderer for color changing
        Renderer rend = flashlight.GetComponent<Renderer>();
        if (rend != null)
        {
            // Use random color for new flashlight
            rend.material.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
