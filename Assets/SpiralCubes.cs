using UnityEngine;
using UnityEngine.AI;

public class SpiralCubes : MonoBehaviour
{
    public int numOfObj = 20;
    public float radiusIncrement = 1.5f;
    public float angleIncrement = 20f;

    void Start()
    {
        SpawnSpiral();
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

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = transform.position + relativePos;
            cube.transform.parent = transform;
            cube.transform.localScale = Vector3.one;

            Renderer rend = cube.GetComponent<Renderer>();
            if (rend != null)
                rend.material.color = new Color(Random.value, Random.value, Random.value);

            NavMeshObstacle obstacle = cube.AddComponent<NavMeshObstacle>();
            obstacle.carving = true;  // Create hole in NavMesh
            obstacle.shape = NavMeshObstacleShape.Box;
            obstacle.size = Vector3.one;
        }
    }
}
