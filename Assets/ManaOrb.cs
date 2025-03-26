using UnityEngine;
using Scripts.CharacterComponents; 

public class ManaOrb : MonoBehaviour
{
    private float manaAmount = 50f;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            Mana playerMana = other.GetComponent<Mana>(); 
            if (playerMana != null)
            {
                playerMana.GainMana(manaAmount); 
                Destroy(gameObject); 
            }
        }
    }
}