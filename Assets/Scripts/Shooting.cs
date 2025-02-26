using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform muzzle;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProjectileShooting();
           
        } 
    }

    void ProjectileShooting()
    {
        Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);
    }

    private void RaycastShooting()
    {
        Vector3 direction = muzzle.forward;
        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, direction, out hit, 1000))
        {
            
        }
    }
}

