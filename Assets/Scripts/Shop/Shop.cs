using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    public GameObject shopUI; 
    private bool isPlayerNear = false;
    private bool isShopOpen = false;
    private ShopManager shopManager;
    private PlayerInputActions playerInputActions;
    private InputAction interactAction;

    public List<Button> shopSlotButtons;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        interactAction = playerInputActions.Player.ShopKey;
        interactAction.performed += ctx => ToggleShop(); 
    }

    void Start()
    {
        if (shopManager == null)
        {
            shopManager = GetComponent<ShopManager>();
        }
        
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

    private void BuyFromShop(int slotIndex)
    {
        if (isShopOpen && slotIndex != -1) 
        {
            shopManager.BuySpell(slotIndex); 
            Debug.Log($"Bought spell in slot {slotIndex}");
        }
        else
        {
            Debug.LogWarning("No valid spell selected to buy.");
        }
    }
}
