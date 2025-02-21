using UnityEngine;
namespace Fred.Code.CharacterComponents
{
    public class Mana: MonoBehaviour
    {
        private float currentMana;
        private float maxMana;
        private float manaRegen;
        
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
        
        private void AdjustMana(float amount)
        {
            currentMana += amount;
        }
        
        public void SpendMana(float mana)
        {
            if(currentMana-mana>=0){
                AdjustMana(-mana);
            }else{
                Debug.Log("Not enough mana");
            }
        }
        
        public void GainMana(float mana)
        {
            if (currentMana + mana <= maxMana)
            {
                AdjustMana(mana);
            }else{
                currentMana = maxMana;
                Debug.Log("Mana is full");
            }
        }
        
        
        
    }
}