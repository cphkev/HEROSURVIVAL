using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public GameObject shopUI; // Reference to the Shop UI (assign in Inspector)
    private bool isPlayerNear = false;
    private bool isShopOpen = false;
    private ShopManager shopManager;
    private PlayerInputActions playerInputActions;
    private InputAction interactAction;

    public List<Button> shopSlotButtons;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        interactAction = playerInputActions.Player.Interact;
        interactAction.performed += ctx => ToggleShop(); // Trigger shop toggle when interact button is pressed
    }

    void Start()
    {
        if (shopManager == null)
        {
            shopManager = GetComponent<ShopManager>();
        }
        
        AddListeners(); // Add listeners for each spell slot button
    }

    void AddListeners()
    {
        // Adding listeners for each spell slot button
        for (int i = 0; i < shopSlotButtons.Count; i++)
        {
            int buttonIndex = i; // Store the index of the button to prevent closure issues
            shopSlotButtons[i].onClick.AddListener(() => BuyFromShop(buttonIndex)); // Pass index to BuyFromShop when clicked
        }
    }

    void OnEnable()
    {
        playerInputActions.Enable(); // Enable input actions
    }

    void OnDisable()
    {
        playerInputActions.Disable(); // Disable input actions
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true; // Player is near the shop
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false; // Player left the shop
            if (isShopOpen)
            {
                ToggleShop(); // Close shop if player leaves
            }
        }
    }

    private void ToggleShop()
    {
        if (isPlayerNear && shopUI != null)
        {
            isShopOpen = !isShopOpen; // Toggle shop visibility
            shopUI.SetActive(isShopOpen);
        }
        else if (shopUI == null)
        {
            Debug.LogError("Shop UI is not assigned in the Shop script!");
        }
    }

    private void BuyFromShop(int slotIndex)
    {
        if (isShopOpen && slotIndex != -1) // Ensure the shop is open and the slot index is valid
        {
            shopManager.BuySpell(slotIndex); // Call the shop manager's BuySpell method with the slot index
            Debug.Log($"Bought spell in slot {slotIndex}");
        }
        else
        {
            Debug.LogWarning("No valid spell selected to buy.");
        }
    }
}
