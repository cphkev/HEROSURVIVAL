using UnityEngine;
using UnityEngine.UI; // For UI elements

public class ManaBar : MonoBehaviour
{
    public Slider manaSlider; // Reference to the UI Slider for Mana
    private HPMPDisplay hpDisplay; // Reference to HPDisplay

    void Start()
    {
        hpDisplay = FindFirstObjectByType<HPMPDisplay>();

        if (hpDisplay == null)
        {
            Debug.LogWarning("HPDisplay not found! Make sure it's active in the scene.");
        }

        if (manaSlider == null)
        {
            Debug.LogWarning("Mana Slider not assigned in the Inspector!");
        }

        UpdateManaBar();
    }

    void Update()
    {
        UpdateManaBar();
    }

    void UpdateManaBar()
    {
        if (hpDisplay != null && manaSlider != null)
        {
            manaSlider.value = hpDisplay.GetCurrentMana();
            manaSlider.maxValue = hpDisplay.GetMaxMana();
        }
    }
}
