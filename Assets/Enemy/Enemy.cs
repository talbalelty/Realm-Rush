using UnityEngine;

// This script handles the Enemy to Bank transactions
public class Enemy : MonoBehaviour
{
    [Header("Balance Configuration")]
    [Tooltip("When enemy is killed")]
    [SerializeField] int reward = 25;
    [Tooltip("When enemy reaches destination")]
    [SerializeField] int penalty = 25;

    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void Reward()
    {
        if (bank == null)
        {
            return;
        }
        bank.Deposit(reward);
    }

    public void Penalty()
    {
        if (bank == null)
        {
            return;
        }
        bank.Withdraw(penalty);
    }
}
