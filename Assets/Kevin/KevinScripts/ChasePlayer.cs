using UnityEngine;

public class ChasePlayer : MonoBehaviour
{

    public float speed = 5.0f;
    public float minDistance = 1.0f;

    public float maxDistance = 10.0f;

    private bool isAttacking = false;
    



    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance <= maxDistance)
            {
                if (distance > minDistance)
                {
                    transform.LookAt(player.transform);
                    transform.position += transform.forward * speed * Time.deltaTime;
                }
                else
                {
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        AttackPlayer();
                    }
                }
            }
        }



    
    }

     void AttackPlayer()
    {
        // Placeholder for attack logic
        Debug.Log("Swinging sword...");
    
    }
}