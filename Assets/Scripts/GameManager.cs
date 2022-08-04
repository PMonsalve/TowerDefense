using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static List<GameObject> enemyGroup = new List<GameObject>();
    [SerializeField]
    private int money, score; //Contagem de pontos e dinheiro

    [SerializeField]
    private Text scoreText, moneyText;

    private bool alive;//variável para definir se o jogo segue ou se deu game over

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        alive = true;
        money = 10;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        moneyText.text = "Money: " + money;  
    }

    public void spendMoney(int value)
    {
        money -= value;
    }
    public void makeMoney(int value)
    {
        money += value;
    }

    public int GetMoney()
    {
        return money;
    }

    public void morePoints(int value)
    {
        score += value;
    }

    public int GetScore()
    {
        return score;
    }

    public bool GetAlive()
    {
        return alive;
    }
    public void setGameOver()
    {
        alive = false;
    }
}
