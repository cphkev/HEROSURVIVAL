using UnityEngine;
using UnityEngine.UI; 

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; 
    private HPMPDisplay hpDisplay; 

    void Start()
    {
        hpDisplay = FindFirstObjectByType<HPMPDisplay>();

        if (hpDisplay == null)
        {
            Debug.LogWarning("HPDisplay not found! Make sure it's active in the scene.");
        }

        if (healthSlider == null)
        {
            Debug.LogWarning("Health Slider not assigned in the Inspector!");
        }

        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (hpDisplay != null && healthSlider != null)
        {
            healthSlider.value = hpDisplay.GetCurrentHP();
            healthSlider.maxValue = hpDisplay.GetMaxHP();
        }
    }
}
