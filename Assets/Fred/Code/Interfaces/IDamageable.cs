public interface IDamageable
{
    int CurrentHP { get; set; }
    int MaxHP { get; set; }
    
    void TakeDamage(int damage);
    void Die();
    void Heal(int amount);
    void UpdateHPUIDisplay();
    
}
