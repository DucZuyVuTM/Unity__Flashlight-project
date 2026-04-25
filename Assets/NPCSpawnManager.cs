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
    public Transform leftTarget;
    public Transform rightTarget;

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
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(spawnRadiusFrom, spawnRadiusTo);

        Vector3 spawnPos = new Vector3(
            randomDirection.x * randomDistance,
            0.5f,
            randomDirection.y * randomDistance
        );

        RaycastHit hit;
        if (Physics.Raycast(spawnPos + Vector3.up * 2f, Vector3.down, out hit, 5f))
        {
            Debug.Log("Raycast hit: " + hit.collider.name + " | Tag: " + hit.collider.tag);

            if (hit.collider.CompareTag("FloorZone") && !IsZoneSafe(hit.collider.gameObject))
            {
                GameObject npcObj = Instantiate(NPC, spawnPos, Quaternion.identity);

                NPC_Chaser chaser = npcObj.GetComponent<NPC_Chaser>();
                if (chaser != null)
                {
                    chaser.leftTarget = leftTarget;
                    chaser.rightTarget = rightTarget;
                }
            }
            else
            {
                Debug.Log("Does not hit FloorZone! Hit: " + hit.collider.tag);
            }
        }
        else
        {
            Debug.Log("Raycast does not hit anything!");
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
