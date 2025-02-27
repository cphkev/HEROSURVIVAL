using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform muzzle;
    private Fireball fireball;

    private void Start()
    {
        // Get Fireball component from prefab or existing object
        fireball = FindObjectOfType<Fireball>();

        if (fireball != null)
        {
            fireball.OnCastFireball += FireBallPrefabShooting;
        }
        else
        {
            Debug.LogError("No Fireball instance found in the scene!");
        }
    }

    private void FireBallPrefabShooting()
    {
        Instantiate(fireBallPrefab, muzzle.position, muzzle.rotation);
    }
}