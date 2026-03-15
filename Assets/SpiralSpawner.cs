using System.Collections;
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
                1.0f, // SỬA: Thay vì 0, hãy để 1.0 để nó nổi hẳn lên giống script cũ
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            GameObject flashlight = Instantiate(flashlightPrefab, transform.position + relativePos, Quaternion.identity, transform);

            // SỬA: Đừng dùng Random.value cho Scale vì nó sẽ làm vật thể biến mất
            flashlight.transform.localScale = Vector3.one; // Để kích thước gốc (1,1,1) cho dễ nhìn

            // SỬA: Lấy Renderer từ con vì script cũ của bạn may mắn là model đơn giản, 
            // còn model hiện tại có nhiều lớp con
            Renderer[] rends = flashlight.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rends)
            {
                r.material.color = new Color(Random.value, Random.value, Random.value);
            }
        }
    }
}
