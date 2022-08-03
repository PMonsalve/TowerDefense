using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private GameObject hoverTile;
    [SerializeField]
    private LayerMask mask,towerMask;


    [SerializeField]
    private GameObject towerSelect;

    private GameObject dummyPlacement;
    private bool building, occupiedSlot;

    // Start is called before the first frame update
    void Start()
    {
        Build();
    }

    // Update is called once per frame
    void Update()
    {
        if (building == true)
        {
            if (dummyPlacement != null)
            {
                GetHoverTile();
                if (hoverTile != null)
                    dummyPlacement.transform.position = hoverTile.transform.position;

            }
            if (Input.GetButtonDown("Fire1"))
                PlaceTower();
        }
    }

    public Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void GetHoverTile()
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

    public void Build()
    {
        building = true;
        dummyPlacement = Instantiate(towerSelect);
        if (dummyPlacement.GetComponent<TowerBehaviour>() != null)
            Destroy(dummyPlacement.GetComponent<TowerBehaviour>());
        

    }

    public void endBuild()
    {
        building = false;
        if (dummyPlacement != null)
            Destroy(dummyPlacement);
    }

    public void PlaceTower()
    {
        if (hoverTile != null)
            if (HaveTower() == false)
            {
                GameObject newTowerObject = Instantiate(towerSelect);
                newTowerObject.layer = LayerMask.NameToLayer("Tower");
                newTowerObject.transform.position = hoverTile.transform.position;
                endBuild();
            }

        
    }

    public bool HaveTower()
    {
        occupiedSlot = false;
        Vector2 mousePosition = GetMousePosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, new Vector2(0, 0), 0.1f, towerMask, -100, 100);
        if (hit.collider != null)
            occupiedSlot = true;
        return occupiedSlot;
    }
}
