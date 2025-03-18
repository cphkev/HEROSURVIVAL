using UnityEngine;
using System.Collections;

public class CanvasActivator : MonoBehaviour
{
    public GameObject canvasElement; // Assign your Canvas/UI element in the Inspector
    public float delayTime = 25f;    // Time in seconds before activation

    void Start()
    {
        // Start the coroutine to activate the UI element after delayTime
        StartCoroutine(ActivateCanvasAfterDelay());
    }

    IEnumerator ActivateCanvasAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        if (canvasElement != null)
        {
            canvasElement.SetActive(true); // Activate the UI element
        }
        else
        {
            Debug.LogWarning("Canvas Element is not assigned!");
        }
    }
}
