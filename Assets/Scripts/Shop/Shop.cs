using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;



public class Shop : MonoBehaviour
{
    public GameObject shopUI; // Reference to the Shop UI (assign in Inspector)
    private bool isPlayerNear = false;
    private bool isShopOpen = false;
    private ShopManager shopManager;
    private PlayerInputActions playerInputActions;
    private InputAction interactAction;
    private InputAction buyAction;
    
    public List<Button> shopSlotButtons;

    private int hoveredSpellSlot = -1; // Store hovered spell index

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        interactAction = playerInputActions.Player.Interact;
        buyAction = playerInputActions.Player.Buy;

        interactAction.performed += ctx => ToggleShop();
        buyAction.performed += BuyFromShop;
    }
    
    void Start()
    {
        if (shopManager == null)
        {
            shopManager = GetComponent<ShopManager>();
            AddListeners();
        }
        
    }

    void AddListeners()
{
    for (int i = 0; i < shopSlotButtons.Count; i++)
    {
        int index = i; // Capture the current value of 'i' in a local variable
        shopSlotButtons[i].onClick.AddListener(() => BuyFromShop(index));
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
            isPlayerNear = false;
            if (isShopOpen)
            {
                ToggleShop();
            }
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

    private void BuyFromShop(InputAction.CallbackContext context)
    {
        if (isShopOpen && hoveredSpellSlot != -1) // Buy only if hovering a valid slot
        {
            shopManager.BuySpell(hoveredSpellSlot);
            Debug.Log($"Bought spell in slot {hoveredSpellSlot}");
        }
        else
        {
            Debug.LogWarning("No spell Selected to buy.");
        }
    }

    // Methods to track hovered spell slot
    public void OnHoverEnter(int slotIndex)
    {
        hoveredSpellSlot = slotIndex;
    }

    public void OnHoverExit()
    {
        hoveredSpellSlot = -1;
    }
}
