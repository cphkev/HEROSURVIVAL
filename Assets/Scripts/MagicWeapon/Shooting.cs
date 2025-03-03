using System.Collections.Generic;
using Scripts.Interfaces;
using Scripts.Spells;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform muzzle;
    private Fireball fireball;

    private void Start()
    {
        // Get Fireball component from prefab or existing object
        List<ISpell> spells = Spellbook.GetAllSpells();
        
        if(spells == null || spells.Count == 0)
        {
            Debug.LogError("No spells found in the Spellbook!shooting");
            return;
        }
        
        fireball = (Fireball) spells[0];

        Debug.Log(spells[0]);
        Debug.Log(fireball);
        
        if (fireball != null)
        {
            Debug.Log("Fireball component found.");
            fireball.OnCastFireball += FireBallPrefabShooting;
        }
        else
        {
            Debug.LogError("No Fireball instance found in the scene!");
        }
    }

    private void FireBallPrefabShooting()
    {
        Instantiate(fireBallPrefab, muzzle.position, muzzle.rotation);
    }
}