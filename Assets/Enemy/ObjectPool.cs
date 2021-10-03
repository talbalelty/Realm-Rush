using System.Collections;
using UnityEngine;

// This script creates a pool of enemies to be spawned together

public class ObjectPool : MonoBehaviour
{
    [Tooltip("The enemy that will be in the pool")]
    [SerializeField] GameObject enemy;
    [Header("Spawn Configuration")]
    [SerializeField] [Range(0, 50)] int poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float spawnDelay = 1f;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    // Creates the pool's objects without activating them
    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // If enemy was kill or reached it's final destination respawn them
    void EnableObjectsInPool()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }
}
