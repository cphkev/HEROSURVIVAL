using UnityEngine;

namespace Scripts.CharacterComponents.EnemyOnlyComponents
{
    public class EnemyComponentTest : MonoBehaviour
    {
        [SerializeField] private float enemyHealth = 50f;

        public void TakeDamage(float damage)
        {
            if (damage <= 0) return;

            enemyHealth -= damage;
            Debug.Log($"Enemy took {damage} damage! Remaining HP: {enemyHealth}");

            if (enemyHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemy has been defeated!");
            Destroy(gameObject);
        }
    }
}
