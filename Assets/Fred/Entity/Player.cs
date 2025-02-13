namespace Fred.Entity
{
    public class Player
    {
        
        int STR = 0;
        int DEX = 0;
        int INT = 0;
        int LUCK = 0;
        
        int HP = 30;
        int currentHP = 30;
        float mana = 100;
        float currentMana = 100;
        float manaRegen = 1;
        float critRate = 0;
        float critDamage = 0;
        int dodge = 0;
        float dodgeChance = 0;
        float attackSpeed = 1;
        float movementSpeed = 1;
        int currency = 0;
        int baseDmg = 0;
        
        
        
        
        public Player(int str, int dex, int intel, int luck)
        {
            STR = str;
            DEX = dex;
            INT = intel;
            LUCK = luck;
        }
        
        
    }
}