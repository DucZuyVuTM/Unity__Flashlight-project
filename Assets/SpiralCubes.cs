using UnityEngine;

public class SpiralCubes : MonoBehaviour
{
    public int numOfObj = 20;
    public float radiusIncrement = 1.5f; // Khoảng cách giãn ra
    public float angleIncrement = 20f;   // Độ xoắn

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

            // Nâng Y lên 1.0 để không bị chìm dưới sàn
            Vector3 relativePos = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                1.0f, 
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            // TẠO CUBE MẶC ĐỊNH: Không cần kéo Prefab vào Inspector nữa
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            // Đặt vị trí và làm con của máy đẻ
            cube.transform.position = transform.position + relativePos;
            cube.transform.parent = transform;

            // Đặt Scale cố định là 1 để dễ nhìn
            cube.transform.localScale = Vector3.one;

            // Đổi màu ngẫu nhiên (sẽ tạo ra Material (Instance) tự động)
            Renderer rend = cube.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = new Color(Random.value, Random.value, Random.value);
            }

            Rigidbody rb = cube.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            cube.isStatic = true;

            cube.layer = LayerMask.NameToLayer("Cubes");
        }
    }
}