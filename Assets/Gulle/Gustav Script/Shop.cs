using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopUI; // Reference to the Shop UI (assign in Inspector)
    private bool isPlayerNear = false;

    void Update()
    {
        // Toggle shop UI when player presses "P" and is near the shop
        if (isPlayerNear && Input.GetKeyDown(KeyCode.P))
        {
            ToggleShop();
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
            shopUI.SetActive(!shopUI.activeSelf);
        }
        else
        {
            Debug.LogError("Shop UI is not assigned in the Shop script!");
        }
    }
}
