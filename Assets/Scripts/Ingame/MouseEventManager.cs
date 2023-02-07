using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseEventManager : MonoBehaviour
{
    public TowerSpawner towerSpawner;
    public Tilemap tilemap;

    private Ray ray;
    private RaycastHit hit;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int c0 = tilemap.WorldToCell(mouseWorldPos);

            towerSpawner.SpawnTower(c0);
            
            // ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            // {
            //     if (hit.transform.CompareTag("Tile"))
            //     {
            //         towerSpawner.SpawnTower(hit.transform);
            //     }
            // }
        }
    }
}
