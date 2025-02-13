public interface IDamageable
{
    // Method to take damage
    void TakeDamage(float damage);
    
    // Optional: You could add a method for healing too, if necessary.
    void Heal(float amount);
}
