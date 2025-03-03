using System;
using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.UI;
using Scripts.Interfaces;
using Scripts.CharacterComponents;
using UnityEngine.InputSystem;

namespace Scripts.CharacterComponents.PlayerOnly
{
    public class PlayerSpells : MonoBehaviour
    {
        [SerializeField] private Spell spell0;
        [SerializeField] private Spell spell1;
        [SerializeField] private Spell spell2;
        [SerializeField] private Spell spell3;
        
        [SerializeField] private Transform castPoint;
        private bool isCasting0;
        private bool isCasting1;
        private bool isCasting2;
        private bool isCasting3;
        
        [SerializeField] private float timeBetweenCasts = 0.25f;
        private float currentCastTimer0;
        private float currentCastTimer1;
        private float currentCastTimer2;
        private float currentCastTimer3;
        
        
        private PlayerInputActions playerInputActions;
        
        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            playerInputActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
        }


        private void Update()
        {
            HandleSpellCasting(spell0, ref isCasting0, playerInputActions.Player.Spell0.ReadValue<float>() > 0.1f, ref currentCastTimer0);
            HandleSpellCasting(spell1, ref isCasting1, playerInputActions.Player.Spell1.ReadValue<float>() > 0.1f, ref currentCastTimer1);
            HandleSpellCasting(spell2, ref isCasting2, playerInputActions.Player.Spell2.ReadValue<float>() > 0.1f, ref currentCastTimer2);
            HandleSpellCasting(spell3, ref isCasting3, playerInputActions.Player.Spell3.ReadValue<float>() > 0.1f, ref currentCastTimer3);
        }

        private void HandleSpellCasting(Spell spell, ref bool isCasting, bool isSpellHeldDown, ref float currentCastTimer)
        {
            bool hasManaEnough = gameObject.GetComponent<Mana>().CurrentMana >= spell.SpellToCast.ManaCost;
    
            if (!isCasting && isSpellHeldDown && hasManaEnough)
            {
                isCasting = true;
                currentCastTimer = 0;
                CastSpell(spell);
            }
    
            if (isCasting)
            {
                currentCastTimer += Time.deltaTime;
                if (currentCastTimer >= timeBetweenCasts)
                {
                    isCasting = false;
                }
            }
        }
        private void CastSpell(Spell spell)
        {
            gameObject.GetComponent<Mana>().SpendMana(spell.SpellToCast.ManaCost);
            Instantiate(spell, castPoint.position, castPoint.rotation);
        }
      
    }
    
}
