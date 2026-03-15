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
        agent = GetComponent<NavMeshAgent>();
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
                Vector3 flatTarget = new Vector3(player.position.x, transform.position.y, player.position.z);

                if (agent.isOnNavMesh) 
                {
                    agent.SetDestination(flatTarget);
                }
            }

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; 
            
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (agent != null && agent.enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, agent.destination);

            Gizmos.DrawSphere(agent.destination, 0.2f);
        }
    }
}
