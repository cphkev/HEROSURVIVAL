using Scripts.CharacterComponents;
using UnityEngine;

namespace Scripts.Interfaces
{
    public interface ISpell
    {
        string SpellName { get; }
        int ManaCost { get; }
        Sprite SpellIcon { get; }

        bool CanCast(float currentMana);
        int CastSpell();
    }
}