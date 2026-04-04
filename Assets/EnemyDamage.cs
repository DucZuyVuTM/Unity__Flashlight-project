using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount = 10f;
    public float damageInterval = 1f;
    private float nextDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "LeftTarget" || other.gameObject.name == "RightTarget")
        {
            if (Time.time >= nextDamageTime)
            {
                PlayerStats stats = other.GetComponentInParent<PlayerStats>();
                
                if (stats != null)
                {
                    stats.TakeDamage(damageAmount);
                    Debug.Log("Health decreased.");

                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }
}
