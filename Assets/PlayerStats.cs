using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;        
        healthBarFill.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Игрок погиб!");        
        SceneManager.LoadScene(0);
    }
}
