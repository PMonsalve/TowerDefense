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

    

    private int currentTile;
    private float distance;


    // Start is called before the first frame update
    void Start()
    {
        enemyStart();
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

    private void checkPosition() //checar posi��o para atualiza��o do movimento
    {
        if (LevelManager.roadTiles[currentTile+1].tag == "Finish")
        {
            Debug.Log("Game Over!");
            Destroy(transform.gameObject);
        }
        if (nextTile != null && LevelManager.roadTiles[currentTile+1].tag !="Finish")
        {
            distance = (transform.position - nextTile.transform.position).magnitude; //Se chegar a distancia de 0.001f, � considerado no pr�ximo tile
            if(distance < 0.001f)
            {
                
                currentTile = LevelManager.roadTiles.IndexOf(nextTile); //Atualiza a posi��o para o proximo tile
                
                nextTile = LevelManager.roadTiles[currentTile + 1]; //Atualiza o proximo tile
                
            }
        }   
    }

    public void takeDamage(float damage) //Metodo para contagem de dano
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
