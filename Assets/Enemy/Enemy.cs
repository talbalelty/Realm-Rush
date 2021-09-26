using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int reward = 25;
    [SerializeField] int penalty = 25;

    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
