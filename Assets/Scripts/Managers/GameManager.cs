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
       
    }


   
}
