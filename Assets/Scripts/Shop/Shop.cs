using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using Scripts.CharacterComponents.PlayerOnly;
public class Shop : MonoBehaviour
{
    public GameObject shopUI; 
    private bool isPlayerNear = false;
    private bool isShopOpen = false;
    private PlayerInputActions playerInputActions;
    private InputAction interactAction;
    private GameObject player;
    public List<Button> shopSlotButtons;
    
    [SerializeField] private Spell spell0;
    [SerializeField] private Spell spell1;
    [SerializeField] private Spell spell2;
    [SerializeField] private Spell spell3;
    [SerializeField] private Spell spell4;
    [SerializeField] private Spell spell5;
    [SerializeField] private Spell spell6;
    [SerializeField] private Spell spell7;
    [SerializeField] private Spell spell8;

    public List<Spell> AllSpells;
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        interactAction = playerInputActions.Player.ShopKey;
        interactAction.performed += ctx => ToggleShop(); 
        
    }

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
       
        InitializeAllSpells();
        InitializeShop();
        AddListeners(); 
    }

    void AddListeners()
    {
        
        for (int i = 0; i < shopSlotButtons.Count; i++)
        {
            int buttonIndex = i; 
            shopSlotButtons[i].onClick.AddListener(() => BuyFromShop(buttonIndex)); 
        }
    }

    void OnEnable()
    {
        playerInputActions.Enable(); 
    }

    void OnDisable()
    {
        playerInputActions.Disable(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isShopOpen)
            {
                ToggleShop(); 
            }
            isPlayerNear = false; 
        }
    }

    private void ToggleShop()
    {
        if (isPlayerNear && shopUI != null)
        {
            isShopOpen = !isShopOpen; 
            shopUI.SetActive(isShopOpen);
        }
        else if (shopUI == null)
        {
            Debug.LogError("Shop UI is not assigned in the Shop script!");
        }
    }

    private void BuyFromShop(int slotIndex)
    {
        if (isShopOpen && slotIndex != -1) 
        {
            BuySpell(slotIndex); 
        }
        else
        {
            Debug.LogWarning("No valid spell selected to buy.");
        }
    }
    private void BuySpell(int slotIndex)
    {
        // Check if the spell list is valid and if the slotIndex is valid
        if (AllSpells == null || AllSpells.Count == 0 || slotIndex < 0 || slotIndex >= AllSpells.Count)
        {
            Debug.LogWarning($"Invalid spell list or slot index: {slotIndex}. Cannot buy spell.");
            return;
        }

        // Ensure the player exists and has the PlayerSpells component
        PlayerSpells playerSpells = player?.GetComponent<PlayerSpells>();
        if (playerSpells == null)
        {
            Debug.LogWarning("Player or PlayerSpells component not found!");
            return;
        }

        // If the spell exists at the given index, equip it
        Spell spellToBuy = AllSpells[slotIndex];
        if (spellToBuy != null)
        {
            playerSpells.EquipSpell(spellToBuy); // Equip the spell in the right slot
            Debug.Log($"Player bought {spellToBuy.SpellToCast.SpellName}!");
        }
        else
        {
            Debug.LogWarning($"Spell is null for slot {slotIndex}. Cannot buy spell.");
        }
    }
    private void InitializeShop()
    {
        // Initialize buttons dynamically with spell names and images
        for (int i = 0; i < shopSlotButtons.Count; i++)
        {
            if (i < AllSpells.Count && AllSpells[i] != null)
            {
                Spell spell = AllSpells[i];

                // Update button image
                Image buttonImage = shopSlotButtons[i].transform.Find("Spellicon")?.GetComponent<Image>();
                if (buttonImage != null && spell.SpellToCast.SpellIcon != null)
                {
                    buttonImage.sprite = spell.SpellToCast.SpellIcon;
                    buttonImage.enabled = true; // Ensure it's visible
                }
                else
                {
                    Debug.LogWarning($"No Image component found in 'Spellicon' for button {i} or missing icon.");
                }
            }
            else
            {
                Debug.LogWarning($"No spell assigned for slot {i}");
            }
        }
    }
    private void InitializeAllSpells()
    {
        AllSpells.Add(spell0);
        AllSpells.Add(spell1);
        AllSpells.Add(spell2);
        AllSpells.Add(spell3);
        AllSpells.Add(spell4);
        AllSpells.Add(spell5);
        AllSpells.Add(spell6);
        AllSpells.Add(spell7);
        AllSpells.Add(spell8);
    }
}
