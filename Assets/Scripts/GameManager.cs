using Scripts.Interfaces;
using System.Collections.Generic;
using Scripts.CharacterComponents;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance; // Singleton instance
    
    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this; // Set the instance
            DontDestroyOnLoad(gameObject); // Optional: Don't destroy the GameManager on scene changes
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one GameManager in the scene
        }
    }

    void Start()
    {
        Invoke("UpdateUI", 0.5f); // Delayed UI update
    }
    
    private void UpdateUI()
    {
        HPMPDisplay ui = FindFirstObjectByType<HPMPDisplay>();
        if (ui != null)
        {
            Debug.Log("UI Found. Updating Stats.");
            ui.UpdateStatsDisplay();
        }
        else
        {
            Debug.LogWarning("HPDisplay not found! Make sure it exists in the scene.");
        }
    }

   
}
