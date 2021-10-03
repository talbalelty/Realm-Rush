using System.Collections.Generic;
using UnityEngine;

// This script handles enemy health and difficulty ramping up

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Vitals")]
    [SerializeField] float maxHealth = 5;
    [Tooltip("Adds health to maxHealth when enemy dies")]
    [SerializeField] int difficultRamp = 1;

    float health;
    List<Vector4> damage = new List<Vector4>();
    Enemy enemy;

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
        // Get damage amounts from particle
        other.GetComponent<ParticleSystem>().GetCustomParticleData(damage, ParticleSystemCustomData.Custom1);
        if (damage.Count == 1)
        {
            health -= Mathf.Abs(damage[0].x);
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            maxHealth += difficultRamp;
            enemy.Reward();
        }
    }
}
