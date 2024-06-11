using UnityEngine;

public class EnemyAIMeleeTypeFunctions : MonoBehaviour
{
    public void SimpleMelee(GameObject target, float meleeRange)
    {
        // Verifica se il bersaglio è abbastanza vicino per essere attaccato
        float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= meleeRange)
        {
            // Infliggi danni al bersaglio
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
        }
    }
}