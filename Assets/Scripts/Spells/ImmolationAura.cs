using UnityEngine;
using System.Collections;
using Scripts.Interfaces;

public class ImmolationAura : MonoBehaviour, ISpell
{
    public string SpellName => "ImmolationAura";
    public int ManaCost => 5; // Per second
    public int SpellDamage => 10; // Per second
    public float radius = 5f;
    public float tickRate = 1f; // Damage every second

    private bool isActive = false;
    private Coroutine auraCoroutine;

   private Sprite spellIcon;

    public Sprite SpellIcon
    {
        get
        {
            if (spellIcon == null)
            {
                
                string path = $"Sprites/{SpellName}";  
                Debug.Log($"Attempting to load sprite from path: {path}");

                // Load sprite from Resources folder
                spellIcon = Resources.Load<Sprite>(path);

                if (spellIcon == null)
                {
                    Debug.LogWarning($"Failed to load sprite for {SpellName} from path: {path}");
                }
            }
            return spellIcon;
        }
    }

    public bool CanCast(float currentMana)
    {
        return currentMana >= ManaCost;
    }

    public int CastSpell()
    {
        if (!isActive)
        {
            isActive = true;
            auraCoroutine = StartCoroutine(ApplyAura());
            Debug.Log($"{SpellName} activated!");
        }
        return 0; // Direct damage not applicable
    }

    public void StopAura()
    {
        if (isActive)
        {
            isActive = false;
            if (auraCoroutine != null)
                StopCoroutine(auraCoroutine);
            Debug.Log($"{SpellName} deactivated!");
        }
    }

    private IEnumerator ApplyAura()
    {
        while (isActive)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider enemy in enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(SpellDamage);
                }
            }
            yield return new WaitForSeconds(tickRate);
        }
    }
}