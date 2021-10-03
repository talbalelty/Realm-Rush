using UnityEngine;

// This script is used to control the Tower behaviour.
// In order for this script to work correctly, it needs to be first in the game object

public class TowerController : MonoBehaviour
{
    [Tooltip("The part of the model that rotates towards the enemy")]
    [SerializeField] Transform weapon;
    [Tooltip("Maximum attack range of the Tower")]
    [SerializeField] float range = 15;

    float maxDistance = Mathf.Infinity;
    float targetDistance;
    ParticleSystem projectileParticles;
    Transform target;
    Transform closestTarget;

    // Start is called before the first frame update
    void Start()
    {
        projectileParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        closestTarget = null;
        maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                // In the end maxDistance will holds the distance to closest target
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void AimWeapon()
    {
        // Keep looking at enemy even if out of range
        weapon.LookAt(target);
        if (maxDistance <= range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        if (projectileParticles != null)
        {
            var emissionModule = projectileParticles.emission;
            emissionModule.enabled = isActive;
        }
    }
}
