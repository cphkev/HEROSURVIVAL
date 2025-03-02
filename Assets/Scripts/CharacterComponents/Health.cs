using UnityEngine;
using Scripts.Interfaces;
namespace Scripts.CharacterComponents
{
    public class Health: MonoBehaviour
    {
       [SerializeField] private int currentHP;
       [SerializeField] private int maxHP;
        
        public int CurrentHP
        {
            get => currentHP;
            set => currentHP = Mathf.Clamp(value, 0, maxHP);
        }
        public int MaxHP
        {
            get => maxHP;
            set => maxHP = value;
        }

        public void Initialize(int maxHP)
        {
            currentHP = maxHP;
            this.maxHP = maxHP;
        }
        

        private void AdjustHP(int amount)
        {
            currentHP += amount;
        }
        
        public void TakeDamage(int damage)
        {
            Debug.Log(damage);
            Debug.Log(currentHP);
            
            if (currentHP - damage >= 0)
            {
                AdjustHP(-damage);
            }else{
                currentHP = 0;
                Die();
            }
        }

        // Method to heal the character
        public void Heal(int amount)
        {
            if (currentHP + amount <= maxHP)
            {
            AdjustHP(amount);
            }else{
                currentHP = maxHP;
                Debug.Log("HP is full");
            }
        }
        
        public void Die()
        {
            var destructibles = GetComponents<IDestructible>();
            foreach (var destructible in destructibles)
            {
                destructible.OnDestruction();
            }
            
            Destroy(gameObject);
            
        }
        
        
    }
}