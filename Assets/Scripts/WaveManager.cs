using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private float timeBetweenSpawn;
    private string[] waveCounter;
    private int numberOfWaves=4;

    [SerializeField]
    private int finalSpawn;//numero qualquer de monstros para spawn
    // Start is called before the first frame update
    void Start()
    {
        waveCounter = ReadWave();
        finalSpawn = waveCounter.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void WaveStart()
    {
        StartCoroutine(SpawnEnemies());
    }
    IEnumerator SpawnEnemies()
    {
        for (int j = 0; j <= numberOfWaves; j++)
        {
            if (j == numberOfWaves)
            {
                for (int i = 0; i < finalSpawn; i++)
                {
                    GameObject newEnemy = Instantiate(enemies[int.Parse(waveCounter[i].ToString())]);
                    yield return new WaitForSeconds(timeBetweenSpawn / 4);
                    GameObject newEnemy2 = Instantiate(enemies[int.Parse(waveCounter[i].ToString())]);
                    yield return new WaitForSeconds(timeBetweenSpawn / 4);
                }
            }
            else
            {
                for (int i = 0; i < finalSpawn; i++)
                {
                    GameObject newEnemy = Instantiate(enemies[int.Parse(waveCounter[i].ToString())]);
                    yield return new WaitForSeconds(timeBetweenSpawn);

                }
            }
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
    
    

    private string[] ReadWave() //Leitura do arquivo texto externo com a seed
    {
        TextAsset data = Resources.Load("Waves") as TextAsset;
        string wave = data.text.Replace(Environment.NewLine, string.Empty);
        return wave.Split('+');
    }
}
