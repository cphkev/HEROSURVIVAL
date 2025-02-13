namespace Fred.Code.Interfaces
{
    public interface ISpell
    {
        string SpellName { get; }
        int ManaCost { get; }
        int SpellDamage { get; }

        bool CanCast(float currentMana);
        int CastSpell();
    }
}