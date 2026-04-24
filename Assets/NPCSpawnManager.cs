using UnityEngine;
using System.Collections;

public class NPCSpawnManager : MonoBehaviour
{
    public GameObject NPC;
    public ProceduralFloorZones floorManager;
    
    [Header("Spawn Settings")]
    public float spawnRadiusFrom = 25f;
    public float spawnRadiusTo = 30f;
    public float spawnInterval = 2f;
    public int maxNPCs = 10;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameObject.FindGameObjectsWithTag("NPC").Length < maxNPCs)
            {
                TrySpawnNPC();
            }
        }
    }

    void TrySpawnNPC()
    {
        // 1. Take a random direction (Unit circle)
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // 2. Take a random distance within the range you have set
        float randomDistance = Random.Range(spawnRadiusFrom, spawnRadiusTo);

        // 3. Calculate the final spawn location
        Vector3 spawnPos = new Vector3(
            randomDirection.x * randomDistance, 
            0.5f, 
            randomDirection.y * randomDistance
        );

        // 2. Shoot a Raycast beam down to check the floor area.
        RaycastHit hit;
        if (Physics.Raycast(spawnPos + Vector3.up * 2f, Vector3.down, out hit, 5f))
        {
            // Check if it hits the floor and is NOT the safe zone.
            if (hit.collider.CompareTag("FloorZone"))
            {
                // Check if this area is safe
                // We will check the name or index of the area
                if (!IsZoneSafe(hit.collider.gameObject))
                {
                    Instantiate(NPC, spawnPos, Quaternion.identity);
                }
                else
                {
                    // If you land in the green zone, skip this attempt (or try a different location).
                    Debug.Log("Safe zone - no spawning NPC!");
                }
            }
        }
    }

    bool IsZoneSafe(GameObject zoneObject)
    {
        // Check if the zone name matches the active zone in FloorManager
        string activeZoneName = "Zone_" + GetDirectionName(floorManager.GetActiveZoneIndex());
        return zoneObject.name == activeZoneName;
    }

    string GetDirectionName(int index)
    {
        switch (index)
        {
            case 0: return "North";
            case 1: return "East";
            case 2: return "South";
            case 3: return "West";
            default: return "";
        }
    }
}
