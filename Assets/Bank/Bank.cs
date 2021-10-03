using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] int enemiesKilledGoal = 10;
    [SerializeField] TMP_Text textBalance;

    int enemiesKilled;

    void Awake()
    {
        currentBalance = startingBalance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textBalance.text = $"Balance: {currentBalance}";
    }
    public int CurrentBalance
    {
        get
        {
            return currentBalance;
        }
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        enemiesKilled++;
        if (enemiesKilled == enemiesKilledGoal)
        {
            // Load Next Level
        }
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
}
