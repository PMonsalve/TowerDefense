using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private GameObject hoverTile;
    [SerializeField]    //Layermasks para o raycast
    private LayerMask mask,towerMask;

    public GameObject manager; //GameManager

    [SerializeField]
    private GameObject towerSelect;

    private GameObject dummyPlacement;
    private bool building, occupiedSlot;

    // Start is called before the first frame update
    void Start()
    {
        //startBuild(towerSelect);
    }

    // Update is called once per frame
    void Update()
    {
        if (building == true)//Se estiver no modo de constru��o de torre, a amostra da torre ficar� seguindo o mouse pelos tiles de constru��o
        {
            if (dummyPlacement != null)
            {
                GetHoverTile();
                if (hoverTile != null)
                    dummyPlacement.transform.position = hoverTile.transform.position;

            }
            if (Input.GetButtonDown("Fire1"))
            { //Bot�o do mouse para confirmara a constru��o e posicionar a torre
                PlaceTower();
            }

        }
    }

    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void GetHoverTile()//Tile que est� de baixo do mouse, considera apenas os tiles do grid para as torres
    {
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, mask, -100, 100);
        if (hit.collider != null)
        {
            if (LevelManager.towerSlots.Contains(hit.collider.gameObject))
            {
                hoverTile = hit.collider.gameObject;
            }
        }
    }

    public void startBuild(GameObject towerButton)//Inicio da constru��o de torre, demonstrando a torre a ser colocara no tile
    {
        building = true;
        dummyPlacement = Instantiate(towerButton);
        if (dummyPlacement.GetComponent<TowerBehaviour>() != null)
            Destroy(dummyPlacement.GetComponent<TowerBehaviour>());

        towerSelect = towerButton; //Altera a torre selecionada para a torre que come�ou a constru��o 
    }

    private void endBuild()//Finaliza o processo de contru��o
    {
        building = false;
        if (dummyPlacement != null)
            Destroy(dummyPlacement);
    }

    private void PlaceTower()//Cria a instancia da torre no local e faz a compra
    {
        if (hoverTile != null)
            if (HaveTower() == false)
            {
                GameObject newTowerObject = Instantiate(towerSelect);
                newTowerObject.layer = LayerMask.NameToLayer("Tower");
                newTowerObject.transform.position = hoverTile.transform.position;
                manager.GetComponent<GameManager>().spendMoney(towerSelect.GetComponent<TowerBehaviour>().price);
                endBuild();
            }

        
    }

    private bool HaveTower()//Metodo que mostra a torre nos slots antes de ser posicionada
    {
        occupiedSlot = false;
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);
        if (hit.collider != null)
            occupiedSlot = true;
        return occupiedSlot;
    }
}
