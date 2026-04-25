using UnityEngine;

public class ProceduralFloorZones : MonoBehaviour
{
    public Transform flashlight;
    public Material safeMaterial;
    public Material dangerMaterial;
    public float floorSize = 30f;

    private MeshRenderer[] renderers = new MeshRenderer[4];
    private int activeZoneIndex = -1;

    void Start()
    {
        // 4 corners on the floor
        Vector3 center = Vector3.zero;
        Vector3 northEast = new Vector3(floorSize, 0, floorSize);
        Vector3 northWest = new Vector3(-floorSize, 0, floorSize);
        Vector3 southWest = new Vector3(-floorSize, 0, -floorSize);
        Vector3 southEast = new Vector3(floorSize, 0, -floorSize);

        // Create 4 zones based on vertices
        CreateTriangleZone(0, "North", new Vector3[] { center, northWest, northEast });
        CreateTriangleZone(1, "East", new Vector3[] { center, northEast, southEast });
        CreateTriangleZone(2, "South", new Vector3[] { center, southEast, southWest });
        CreateTriangleZone(3, "West", new Vector3[] { center, southWest, northWest });
    }

    void CreateTriangleZone(int index, string zoneName, Vector3[] vertices)
    {
        GameObject zone = new GameObject("Zone_" + zoneName);
        zone.transform.parent = this.transform;
        zone.transform.localPosition = new Vector3(0, 0.03f, 0);

        MeshFilter mf = zone.AddComponent<MeshFilter>();
        MeshRenderer mr = zone.AddComponent<MeshRenderer>();
        MeshCollider mc = zone.AddComponent<MeshCollider>(); 

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        // Connect the tops clockwise so that the right side is facing upwards
        mesh.triangles = new int[] { 0, 1, 2 }; 
        mesh.RecalculateNormals();

        mf.mesh = mesh;
        mc.sharedMesh = mesh; 
        mr.material = dangerMaterial;

        renderers[index] = mr;
        zone.tag = "FloorZone";  // Use tag so that the NPC can check
    }

    void Update()
    {
        if (flashlight == null) return;

        // Shine the flashlight beam downwards to identify the area it is standing in.
        RaycastHit hit;
        if (Physics.Raycast(flashlight.position, Vector3.down, out hit, 10f))
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (hit.collider.gameObject == renderers[i].gameObject)
                {
                    UpdateZoneColors(i);
                    break;
                }
            }
        }
    }

    void UpdateZoneColors(int safeIndex)
    {
        if (activeZoneIndex == safeIndex) return;
        activeZoneIndex = safeIndex;

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = (i == safeIndex) ? safeMaterial : dangerMaterial;
        }
    }
    
    // Support function for SpawnManager: Returns the current safe zone index
    public int GetActiveZoneIndex() => activeZoneIndex;
}
