using UnityEngine;
using System.Collections;

namespace Fred.Code.CharacterComponents
{
    public class Mana : MonoBehaviour
    {
        private float currentMana;
        private float maxMana;
        private float manaRegen = 5f; // Amount of mana to regen per interval
        private int regenInterval = 3; // Time in seconds between regens

        public float CurrentMana
        {
            get => currentMana;
            set => currentMana = Mathf.Clamp(value, 0, maxMana);
        }

        public float MaxMana
        {
            get => maxMana;
            set => maxMana = value;
        }
        
        public float ManaRegen
        {
            get => manaRegen;
            set => manaRegen = value;
        }
        
        public int RegenInterval { get => regenInterval;}

        public Mana(float maxMana)
        {
            this.currentMana = maxMana;
            this.maxMana = maxMana;
        }

        private void Start()
        {
            StartCoroutine(RegenerateMana());
        }

        private IEnumerator RegenerateMana()
        {
            while (true)
            {
                yield return new WaitForSeconds(regenInterval);
                GainMana(manaRegen);
            }
        }

        private void AdjustMana(float amount)
        {
            currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        }

        public void SpendMana(float mana)
        {
            if (currentMana - mana >= 0)
            {
                AdjustMana(-mana);
            }
            else
            {
                Debug.Log("Not enough mana");
            }
        }

        public void GainMana(float mana)
        {
            AdjustMana(mana);
        }
    }
}