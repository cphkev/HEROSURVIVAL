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
        
        private float currentCastTimer0;
        private float currentCastTimer1;
        private float currentCastTimer2;
        private float currentCastTimer3;
        
        private float currentCooldownTimer0;
        private float currentCooldownTimer1;
        private float currentCooldownTimer2;
        private float currentCooldownTimer3;
        
        private PlayerInputActions playerInputActions;
        
        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            initializeCooldownTimers();
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
            countDownCooldownTimers();
            if(spell0!=null) HandleSpellCasting(spell0, ref isCasting0, playerInputActions.Player.Spell0.ReadValue<float>() > 0.1f, ref currentCastTimer0, ref currentCooldownTimer0);
            if(spell1!=null) HandleSpellCasting(spell1, ref isCasting1, playerInputActions.Player.Spell1.ReadValue<float>() > 0.1f, ref currentCastTimer1, ref currentCooldownTimer1);
            if(spell2!=null) HandleSpellCasting(spell2, ref isCasting2, playerInputActions.Player.Spell2.ReadValue<float>() > 0.1f, ref currentCastTimer2, ref currentCooldownTimer2);
            if(spell3!=null) HandleSpellCasting(spell3, ref isCasting3, playerInputActions.Player.Spell3.ReadValue<float>() > 0.1f, ref currentCastTimer3, ref currentCooldownTimer3);
        }

        private void HandleSpellCasting(Spell spell, ref bool isCasting, bool isSpellHeldDown, ref float currentCastTimer, ref float currentCooldownTimer)
        {
            bool hasManaEnough = gameObject.GetComponent<Mana>().CurrentMana >= spell.SpellToCast.ManaCost;
            
            //If we are not casting and the spell is held down, we start casting
            if (isSpellHeldDown && !isCasting0 && !isCasting1 && !isCasting2 && !isCasting3)
            {
                isCasting = true;
                currentCastTimer = 0;
            }
    
            //If we are casting and we have enough mana and the cooldown is over the currentCastTimer increases
            if (isCasting && hasManaEnough && currentCooldownTimer <= 0)
            {
                //If the spell is not held down, then we stop casting
                if(!isSpellHeldDown)
                {
                    isCasting = false;
                    currentCastTimer = 0;
                }
                
                currentCastTimer += Time.deltaTime;
                
                //If the currentCastTimer is greater or equal than the spell cast time, we cast the spell. The spell goes on cooldown and we stop casting
                if (currentCastTimer >= spell.SpellToCast.CastTime)
                {
                    CastSpell(spell);
                    currentCooldownTimer = spell.SpellToCast.Cooldown;
                    isCasting = false;
                }
            }
        }
        
        private void CastSpell(Spell spell)
        {
            gameObject.GetComponent<Mana>().SpendMana(spell.SpellToCast.ManaCost);
            Instantiate(spell, castPoint.position, castPoint.rotation);
        }
        
        
        private void initializeCooldownTimers()
        {
            currentCooldownTimer0 = 0;
            currentCooldownTimer1 = 0;
            currentCooldownTimer2 = 0;
            currentCooldownTimer3 = 0;
        }
        private void countDownCooldownTimers()
        {
            if (currentCooldownTimer0 > 0)
            {
                currentCooldownTimer0 -= Time.deltaTime;
            }
            if (currentCooldownTimer1 > 0)
            {
                currentCooldownTimer1 -= Time.deltaTime;
            }
            if (currentCooldownTimer2 > 0)
            {
                currentCooldownTimer2 -= Time.deltaTime;
            }
            if (currentCooldownTimer3 > 0)
            {
                currentCooldownTimer3 -= Time.deltaTime;
            }
        }
      
        //Sorry dette er ikke en pæn løsning
        public void EquipSpell(Spell spell)
        {
            if (spell0 == null)
            {
                spell0 = spell;
            }
            else if (spell1 == null)
            {
                spell1 = spell;
            }
            else if (spell2 == null)
            {
                spell2 = spell;
            }
            else if (spell3 == null)
            {
                spell3 = spell;
            }else
            {
                Debug.Log("No more spell slots available");
            }
            
        }

    }
}
