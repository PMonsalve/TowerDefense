using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    private int resultScore, resultMoney;
    public GameObject manager;//gamemanager
    public bool counting=true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (counting)//contador para a tela final de resultados
        {
            resultMoney = manager.GetComponent<GameManager>().GetMoney();
            resultScore = manager.GetComponent<GameManager>().GetScore();
        }

        if (!manager.GetComponent<GameManager>().GetAlive())
        {
            counting = false;
            SceneManager.LoadScene("Scenes/Results");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Stage");
        counting = true;

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/Title");
    }
}
