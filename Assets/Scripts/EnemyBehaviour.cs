using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float enemyHP, enemySpeed;

    [SerializeField]
    private int coins, points;

    [SerializeField]
    private GameObject nextTile;

    public GameObject manager;

    private int currentTile;
    private float distance;


    // Start is called before the first frame update
    void Start()
    {
        enemyStart();
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        checkPosition();
        enemyMovement();
    }

    private void enemyStart() //Primeiro tile que o inimigo vai
    {
        nextTile = LevelManager.roadTiles[0];
    }

    private void enemyMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextTile.transform.position, enemySpeed * Time.deltaTime);
    }

    private void checkPosition() //checar posição para atualização do movimento
    {
        if (LevelManager.roadTiles[currentTile+1].tag == "Finish")
        {
            Debug.Log("Game Over!");
            gameOver();
            Destroy(transform.gameObject);
        }
        if (nextTile != null && LevelManager.roadTiles[currentTile+1].tag !="Finish")
        {
            distance = (transform.position - nextTile.transform.position).magnitude; //Se chegar a distancia de 0.001f, é considerado no próximo tile
            if(distance < 0.001f)
            {
                
                currentTile = LevelManager.roadTiles.IndexOf(nextTile); //Atualiza a posição para o proximo tile
                
                nextTile = LevelManager.roadTiles[currentTile + 1]; //Atualiza o proximo tile
                
            }
        }   
    }

    public void takeDamage(float damage) //Metodo para contagem de dano
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            manager.GetComponent<GameManager>().morePoints(points);
            manager.GetComponent<GameManager>().makeMoney(coins);
            Destroy(gameObject);
        }
    }
    private void gameOver()
    {
        manager.GetComponent<GameManager>().setGameOver();
    }
}
