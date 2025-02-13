using UnityEngine;
using System; // Required for Action<> delegate

public class Enemy : MonoBehaviour
{
    public event Action OnDeath; // Event to notify when enemy dies

    public void TakeDamage(int damage)
    {
        // Example: Assume enemy has 100 HP
        if (damage >= 100) 
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke(); // Trigger the OnDeath event before destroying the enemy
        Destroy(gameObject);
    }
}
