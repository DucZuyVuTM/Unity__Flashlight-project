using UnityEngine;
using UnityEngine.AI;

public class NPC_Chaser : MonoBehaviour
{
    public Transform player;
    public Transform leftTarget;
    public Transform rightTarget;
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
        if (player != null && leftTarget != null && rightTarget != null)
        {
            if (Time.time >= nextUpdateTime)
            {
                nextUpdateTime = Time.time + updateRate;

                float distToLeft = Vector3.Distance(transform.position, leftTarget.position);
                float distToRight = Vector3.Distance(transform.position, rightTarget.position);

                Transform closestTarget = (distToLeft < distToRight) ? leftTarget : rightTarget;

                if (agent.isOnNavMesh) 
                {
                    agent.SetDestination(closestTarget.position);
                }
            }

            LookAtTarget(player.position);
        }
    }

    void LookAtTarget(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 10f);
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
