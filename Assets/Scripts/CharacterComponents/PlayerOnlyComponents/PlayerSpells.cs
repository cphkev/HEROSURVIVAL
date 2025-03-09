using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.CharacterComponents.PlayerOnly
{
    public class PlayerSpells : MonoBehaviour
    {
        [SerializeField] private Transform castPoint;
        [SerializeField] private Spell[] spells = new Spell[4];

        private int currentCastingIndex = -1; // -1 means no spell is being cast
        private float currentCastTimer = 0;
        private float[] currentCooldownTimers = new float[4];

        private PlayerInputActions playerInputActions;
        private InputAction[] spellActions; // Store InputActions in an array
        private Mana playerMana;

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerMana = GetComponent<Mana>();

            // Initialize spellActions with references to the specific input actions
            spellActions = new InputAction[]
            {
                playerInputActions.Player.Spell0,
                playerInputActions.Player.Spell1,
                playerInputActions.Player.Spell2,
                playerInputActions.Player.Spell3
            };
        }

        private void OnEnable() => playerInputActions.Enable();
        private void OnDisable() => playerInputActions.Disable();

        private void Update()
        {
            CountDownCooldownTimers();

            if (currentCastingIndex == -1) // No spell is being cast, check for new input
            {
                for (int i = 0; i < spells.Length; i++)
                {
                    if (spells[i] != null && spellActions[i].ReadValue<float>() > 0.1f && currentCooldownTimers[i] <= 0)
                    {
                        StartCasting(i);
                        break; // Only allow one spell to start casting
                    }
                }
            }
            else // A spell is currently casting
            {
                ContinueCasting();
            }
        }

        private void StartCasting(int spellIndex)
        {
            Spell spell = spells[spellIndex];
            if (playerMana.CurrentMana < spell.SpellToCast.ManaCost) return; // Not enough mana

            currentCastingIndex = spellIndex;
            currentCastTimer = 0;
        }

        private void ContinueCasting()
        {
            if (currentCastingIndex == -1) return; // No spell is being cast

            Spell spell = spells[currentCastingIndex];

            if (spellActions[currentCastingIndex].ReadValue<float>() <= 0.1f) // If button is released, cancel casting
            {
                currentCastingIndex = -1;
                currentCastTimer = 0;
                return;
            }

            currentCastTimer += Time.deltaTime;

            if (currentCastTimer >= spell.SpellToCast.CastTime)
            {
                CastSpell(spell);
                currentCooldownTimers[currentCastingIndex] = spell.SpellToCast.Cooldown;
                currentCastingIndex = -1; // Reset casting state
            }
        }

        private void CastSpell(Spell spell)
        {
            playerMana.SpendMana(spell.SpellToCast.ManaCost);
            Instantiate(spell, castPoint.position, castPoint.rotation);
        }

        private void CountDownCooldownTimers()
        {
            for (int i = 0; i < currentCooldownTimers.Length; i++)
            {
                if (currentCooldownTimers[i] > 0)
                {
                    currentCooldownTimers[i] -= Time.deltaTime;
                }
            }
        }

        public void EquipSpell(Spell spell)
        {
            for (int i = 0; i < spells.Length; i++)
            {
                if (spells[i] == null)
                {
                    spells[i] = spell;
                    return;
                }
            }
            Debug.Log("No more spell slots available");
        }
    }
}
