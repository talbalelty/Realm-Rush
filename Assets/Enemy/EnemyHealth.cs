using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 5;

    List<Vector4> damage = new List<Vector4>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
    }

    private void ProcessHit(GameObject other)
    {
        other.GetComponent<ParticleSystem>().GetCustomParticleData(damage, ParticleSystemCustomData.Custom1);
        health -= damage[0].x;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
