using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI; // Reference to the Shop UI (assign in Inspector)
    private bool isPlayerNear = false;
    private bool isShopOpen = false;
    private ShopManager shopManager;

    
    void Start()
    {
        // Ensure ShopManager is assigned
        if (shopManager == null)
        {
            shopManager = GetComponent<ShopManager>();
        }
    }
    void Update()
    {
        // Toggle shop UI when player presses "P" and is near the shop
        if (isPlayerNear && Input.GetKeyDown(KeyCode.P))
        {
            ToggleShop();
        }
        
        if(!isPlayerNear && isShopOpen)
        {
            ToggleShop();
        }

        if (isShopOpen)
        {
            BuyFromShop();
        }
        
    }

    // Called when player enters the shop area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Called when player exits the shop area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    private void ToggleShop()
    {
        if (shopUI != null)
        {
            isShopOpen = !isShopOpen;
            shopUI.SetActive(isShopOpen);
        }
        else
        {
            Debug.LogError("Shop UI is not assigned in the Shop script!");
        }
    }

    private void BuyFromShop()
    {
        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "1":
                    shopManager.BuySpell(1);
                    Debug.Log("1 key pressed");
                    break;
                case "2":
                    shopManager.BuySpell(2);
                    Debug.Log("2 key pressed");
                    break;
                default:
                    // Code to execute when any other key is pressed
                    break;
            }
        }
    }
    
    
}
