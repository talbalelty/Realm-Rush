using System.Collections;
using UnityEngine;

// This script handles the Tower's initial spawn

public class Tower : MonoBehaviour
{
    [Header("Tower Configuration")]
    [SerializeField] int cost = 75;
    [SerializeField] [Range(0f, 10f)] float buildDelay = 3f;

    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Build());
    }

    // Used as a visual representation of building the tower
    IEnumerator Build()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        float partDelay = buildDelay / transform.childCount;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(partDelay);
        }
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false;
    }
}
