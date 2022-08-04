using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tower; //Torre do bot�o

    [SerializeField]    
    private Text price; //pre�o que aparece no bot�o

    [SerializeField]
    private GameObject game; //gamemanager
       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        price.text = tower.GetComponent<TowerBehaviour>().price.ToString();
        this.GetComponent<Image>().sprite = tower.GetComponent<SpriteRenderer>().sprite;
        if (int.Parse(price.text) > game.GetComponent<GameManager>().GetMoney())     //fun��o para desativar e ativar o bot�o de acordo com o dinheiro que possui
            this.GetComponent<Button>().interactable = false;
        else
            this.GetComponent<Button>().interactable = true;
    }

}
