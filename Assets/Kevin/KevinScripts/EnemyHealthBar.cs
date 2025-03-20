using Scripts.CharacterComponents;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Health enemyHealth; // Reference to the Health component

    private void Start()
    {
        if (enemyHealth == null)
        {
            enemyHealth = GetComponentInParent<Health>(); // Auto-assign if not set
        }

        if (enemyHealth != null)
        {
            slider.maxValue = enemyHealth.MaxHP;
            slider.value = enemyHealth.CurrentHP;
        }
    }

    private void Update()
    {
        if (enemyHealth != null)
        {
            slider.value = enemyHealth.CurrentHP;
        }

        // Keep the health bar facing the camera
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
    }
}
