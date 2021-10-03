using UnityEngine;

//In order for this script to work correctly, it needs to be first in the game object
public class TowerController : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15;

    ParticleSystem projectileParticles;
    Transform target;
    Transform closestTarget;
    float maxDistance = Mathf.Infinity;
    float targetDistance;

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
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void AimWeapon()
    {
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
