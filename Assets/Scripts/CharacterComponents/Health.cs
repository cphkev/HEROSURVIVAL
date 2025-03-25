using UnityEngine;
using Scripts.Interfaces;
using TMPro;


namespace Scripts.CharacterComponents

{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int currentHP;
        [SerializeField] private int maxHP;
        public GameObject damageNumberPrefab;


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
            currentHP = Mathf.Max(0, currentHP - damage);
            if (currentHP == 0) Die();

            if (damageNumberPrefab)
            {
                var dmgText = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity)
                    .GetComponent<DamageNumbers>();
                dmgText?.SetDamage(damage);
            }
        }

        // Method to heal the character
        public void Heal(int amount)
        {
            if (currentHP + amount <= maxHP)
            {
                AdjustHP(amount);
            }
            else
            {
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

            // Find SceneLoader and trigger death transition
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            if (sceneLoader != null && gameObject.CompareTag("Player"))
            {
                sceneLoader.OnPlayerDeath(gameObject);
            }else if(sceneLoader != null && gameObject.CompareTag("Gate"))
            {
                sceneLoader.OnGateDeath(gameObject);
            }
            else
            {
                Debug.LogError("SceneLoader not found in the scene!");
            }

            // Only destroy non-player objects
            if (!gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}