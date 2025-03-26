using UnityEngine;


public class TipTrigger : MonoBehaviour
{
    public GameObject tipCanvas; // Reference to your TipCanvas
    public GameObject ShopCam; // Reference to your ShopCam

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object is the player
        {
            tipCanvas.SetActive(true); // Activate the TipCanvas
        }

        if (other.CompareTag("Player")) // Check if the object is the player
        {
            ShopCam.SetActive(true); // Activate the TipCanvas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object is the player
        {
            tipCanvas.SetActive(false); // Deactivate the TipCanvas
        }

        if (other.CompareTag("Player")) // Check if the object is the player
        {
            ShopCam.SetActive(false); // Deactivate the TipCanvas
        }
    }
}

