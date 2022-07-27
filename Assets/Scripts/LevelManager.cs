using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tiles;

    public float TileSize //Acessar o tamanho do tile de forma pública
    {
        get { return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMap() //Metodo de criação e posicionamento dos tiles no mapa
    {
        string[] stageSeed = ReadStageSeed();

        int maxX = stageSeed[0].ToCharArray().Length; // Limites do mapa
        int maxY = stageSeed.Length;
        Vector3 screenCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); //Percorrer e preencher o mapa com tiles
        for (int j = 0; j < maxY; j++)
        {
            char[] newTiles = stageSeed[j].ToCharArray(); //Preencher a linha
            for(int i = 0; i < maxX; i++)
            {
                GameObject newTile = Instantiate(tiles[int.Parse(newTiles[i].ToString())]); //Posicionamento dos tiles de acordo com o numero
                newTile.transform.position = new Vector3(screenCorner.x + TileSize * i, screenCorner.y - TileSize * j, 0);
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
