using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles;

    public static List<GameObject> roadTiles = new List<GameObject>();
    public static List<GameObject> towerSlots = new List<GameObject>();

    private int maxX; // Limites do mapa
    private int maxY;

    public static GameObject spawn;
    public static GameObject finish;

    private GameObject newTile;

    // Start is called before the first frame update
    void Start()
    {
        createMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float TileSize //Acessar o tamanho do tile de forma p�blica
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    private void createMap() //Metodo de criacao e posicionamento dos tiles no mapa
    {
        
        string[] stageSeed = ReadStageSeed();

        maxX = stageSeed[0].ToCharArray().Length; //Limites do mapa de acordo com o tamanho da matriz do arquivo externo(XxY)
        maxY = stageSeed.Length;

        Vector3 screenCorner = Camera.main.ScreenToWorldPoint(new Vector3(95, Screen.height-95)); //Percorrer e preencher o mapa com tiles
        for (int j = 0; j < maxY; j++)
        {
            char[] newTiles = stageSeed[j].ToCharArray(); //Preencher a linha
            for(int i = 0; i < maxX; i++)
            {
                newTile = Instantiate(tiles[int.Parse(newTiles[i].ToString())]); //Posicionamento dos tiles de acordo com o numero
                newTile.transform.position = new Vector3(screenCorner.x + TileSize * i, screenCorner.y - TileSize * j, 0);
                
                if (newTiles[i].ToString() != "0" && newTiles[i].ToString() != "4")
                {   //Atribuir os tiles do caminho para uma lista da estrada, excluindo 0 que é o fundo e 4 que são espaços para torres 
                    roadTiles.Add(newTile);
                    //tiles[int.Parse(newTiles[i].ToString())].transform.position = newTile.transform.position;
                }
                else if(newTiles[i].ToString() == "4")
                    towerSlots.Add(newTile); //Adicionar o tile de espaço para torre na lista
            }
        }
    }

    private string[] ReadStageSeed() //Leitura do arquivo texto externo com a seed
    {
        TextAsset data = Resources.Load("Stages") as TextAsset;
        string seed = data.text.Replace(Environment.NewLine, string.Empty);
        return seed.Split('+');
    }

  

    
}
