using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    public float skyboxRotationSpeed = 1f; // Adjust this to control the speed

    private void Start()
    {
        // Only trigger auto transition if we're in the DeathScene
        if (SceneManager.GetActiveScene().name == "DeathScene")
        {
            StartCoroutine(AutoLoadMainMenuD());
        }

        if (SceneManager.GetActiveScene().name == "Win")
        {
            StartCoroutine(AutoLoadMainMenuW());
        }
    }

    private void Update()
    {
        // Rotate the skybox smoothly
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxRotationSpeed);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        animator.SetTrigger("FadeOut");

        // Wait for the fade-out animation duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator AutoLoadMainMenuD()
    {
        yield return new WaitForSeconds(15f);
        LoadScene("MainMenu");
    }

    private IEnumerator AutoLoadMainMenuW()
    {
        yield return new WaitForSeconds(40f);
        LoadScene("MainMenu");
    }

    public void OnPlayerDeath(GameObject player)
    {
        LoadScene("DeathScene"); 
    }

}
