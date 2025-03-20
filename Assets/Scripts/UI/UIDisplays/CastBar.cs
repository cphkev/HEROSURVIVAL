using UnityEngine;
using UnityEngine.UI; 

public class CastBar : MonoBehaviour
{
    public Slider castSlider; 
    private HPMPDisplay castDisplay; 

    void Start()
    {
        castDisplay = FindFirstObjectByType<HPMPDisplay>();

        if (castDisplay == null)
        {
            Debug.LogWarning("castDisplay not found! Make sure it's active in the scene.");
        }

        if (castSlider == null)
        {
            Debug.LogWarning("Cast Slider not assigned in the Inspector!");
        }

        UpdateCastBar();
    }

    void Update()
    {
        UpdateCastBar();
    }

    void UpdateCastBar()
    {
        if (castDisplay != null && castSlider != null)
        {
            castSlider.value = castDisplay.GetCurrentCastTimer();
            castSlider.maxValue = castDisplay.GetMaxCastTime();
        }
    }
}
