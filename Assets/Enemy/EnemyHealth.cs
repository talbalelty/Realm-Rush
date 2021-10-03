using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 5;
    [Tooltip("Adds health to maxHealth when enemy dies")]
    [SerializeField] int difficultRamp = 1;

    Enemy enemy;
    float health;
    List<Vector4> damage = new List<Vector4>();

    // Start is called before the first frame update
    void OnEnable()
    {
        health = maxHealth;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
    }

    private void ProcessHit(GameObject other)
    {
        other.GetComponent<ParticleSystem>().GetCustomParticleData(damage, ParticleSystemCustomData.Custom1);
        if (damage.Count == 1)
        {
            health -= damage[0].x;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            maxHealth += difficultRamp;
            enemy.Reward();
        }
    }
}
