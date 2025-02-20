using UnityEngine;
namespace Fred.Code.CharacterComponents
{
    public class Mana: MonoBehaviour
    {
        private float currentMana;
        private float maxMana;
        
        public float CurrentMana
        {
            get => currentMana;
            set => currentMana = Mathf.Clamp(value, 0, MaxMana);
        }
        
    }
}