using UnityEngine;
using UnityEngine.AI;

public class NPC_Chaser : MonoBehaviour
{
    public Transform player;
    public float updateRate = 0.02f;

    private NavMeshAgent agent;
    private float nextUpdateTime;

    void Start()
    {
        agent = GetComponent<Rigidbody>().gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            if (Time.time >= nextUpdateTime)
            {
                nextUpdateTime = Time.time + updateRate;
                
                // NavMeshAgent tự hiểu độ cao, chỉ cần truyền player.position
                if (agent.isOnNavMesh) 
                {
                    agent.SetDestination(player.position);
                }
            }

            // --- LOGIC XOAY ---
            if (agent.velocity.sqrMagnitude > 0.1f)
            {
                Vector3 direction = agent.velocity.normalized;
                direction.y = 0;
                
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Vẽ đường nối từ NPC tới đích đến thực tế của Agent
        if (agent != null && agent.enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, agent.destination);
            Gizmos.DrawSphere(agent.destination, 0.2f);
        }
    }
}