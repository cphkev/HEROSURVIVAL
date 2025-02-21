using UnityEngine;
using Fred.Code.Interfaces;
namespace Fred.Code.CharacterComponents
{
    public class Health: MonoBehaviour
    {
        private int currentHP;
        private int maxHP;
        
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
            
            Debug.Log("die lol");
        }
        
        
    }
}