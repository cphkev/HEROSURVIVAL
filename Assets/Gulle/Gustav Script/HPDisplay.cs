using UnityEngine;
using TMPro; // Make sure to import TextMeshPro

public class HPDisplay : MonoBehaviour
{
    public TMP_Text hpText; // Reference to the TextMeshPro component

    private Character playerCharacter;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerCharacter = player.GetComponent<Character>();
            if (playerCharacter == null)
            {
                Debug.LogWarning("Player does not have a Character component!");
            }
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the Player has the correct tag.");
        }

        UpdateHPDisplay();
    }

    void Update()
    {
        if (playerCharacter != null)
        {
            UpdateHPDisplay();
        }
    }

   

    void UpdateHPDisplay()
{
    if (playerCharacter != null && hpText != null)
    {
        // Log the current HP to check its value
        Debug.Log("Updating HP: " + playerCharacter.currentHP);

        // Update the UI text
        hpText.text = "HP: " + playerCharacter.currentHP.ToString();

        // Force an immediate UI refresh
        Canvas.ForceUpdateCanvases();
    }
}

}
