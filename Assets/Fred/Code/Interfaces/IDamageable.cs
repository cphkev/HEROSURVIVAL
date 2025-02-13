public interface IDamageable
{
    int CurrentHP { get; set; }
    void TakeDamage(int damage);
    void Die();
    void Heal(int amount);
    void UpdateHPUIDisplay();
    
}
