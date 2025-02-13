using Fred.Code.Interfaces;
using UnityEngine;
    public class Fireball : MonoBehaviour, ISpell
    {
        public string SpellName => "Fireball";
        public int ManaCost => 20;
        public int SpellDamage => 50;

        public bool CanCast(float currentMana)
        {
            return currentMana >= ManaCost;
        }

        public int CastSpell()
        {
            Debug.Log($"{SpellName} casted! Damage: {SpellDamage}");
            return SpellDamage;
        }
    }