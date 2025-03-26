using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance;
    public TMP_Text killCountText;
    private int killCount = 0;
    
    public int KillCount
    {
        get => killCount;
        set => killCount = value;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterKill(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            killCount++;
            UpdateKillUI();
        }
    }

    public void UpdateKillUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Marbles:" + killCount;
        }
    }
}