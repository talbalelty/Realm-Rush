using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the currency and scene reloads
public class Bank : MonoBehaviour
{

    [SerializeField] TMP_Text textBalance;
    [Header("Game Configuration")]
    [SerializeField] int startingBalance = 800;
    // [SerializeField] int enemiesKilledGoal = 25;

    int currentBalance;
    int enemiesKilled;

    void Awake()
    {
        currentBalance = startingBalance;
    }

    // Update is called once per frame
    void Update()
    {
        textBalance.text = $"Enemies Killed: {enemiesKilled} \nBalance: {currentBalance}";
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        enemiesKilled++;

        // Win the game
        //if (enemiesKilled == enemiesKilledGoal)
        //{
        //    // Load Next Level
        //    Debug.Log("Winner!");
        //    ReloadScene();
        //}
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        //Lose the game
        if (currentBalance < 0)
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public int CurrentBalance
    {
        get
        {
            return currentBalance;
        }
    }
}
