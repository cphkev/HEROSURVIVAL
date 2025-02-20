using UnityEngine;
namespace Fred.Code.CharacterComponents
{
    public class Health: MonoBehaviour
    {
        private int currentHP;
        private int maxHP;
        
        public int CurrentHP
        {
            get => currentHP;
            set => currentHP = Mathf.Clamp(value, 0, MaxHP);
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;

            if (CurrentHP == 0)
            {
                Die();
            }

            Debug.Log($"{characterName} took {damage} damage. Current HP: {CurrentHP}");
        }

        // Method to heal the character
        public void Heal(int amount)
        {
            CurrentHP += amount;
            Debug.Log($"{characterName} healed by {amount}. Current HP: {CurrentHP}");
        }

        // Placeholder for Die() from IDamageable interface
        public void Die()
        {
            Debug.Log($"{characterName} has died.");
        }
        
        
    }
}