using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MouseEventManager : MonoBehaviour
{
    public TowerSpawner towerSpawner;
    public Tilemap tilemap;

    private Ray ray;
    private RaycastHit hit;

    public GameObject hoveredTower;
    public int hoveredTowerLevel = -1;

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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (GameState.Instance.nextTowerLevel < 0) return;

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int c0 = tilemap.WorldToCell(mouseWorldPos);

            towerSpawner.SpawnTower(c0, GameState.Instance.nextTowerLevel);
            GameState.Instance.nextTowerLevel = -1;
            
            // ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            // {
            //     if (hit.transform.CompareTag("Tile"))
            //     {
            //         towerSpawner.SpawnTower(hit.transform);
            //     }
            // }
        }

        if (hoveredTowerLevel != GameState.Instance.nextTowerLevel)
        {
            // Change sprite
            hoveredTowerLevel = GameState.Instance.nextTowerLevel;
            if (hoveredTowerLevel >= 0)
            {
                var prefab = (GameObject)Resources.Load("Prefabs/Towers/Tower" + hoveredTowerLevel.ToString());
                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 location = tilemap.WorldToCell(mousePos);
                location.x += 0.5f;
                location.y += 0.5f;

                if (hoveredTower)
                {
                    Destroy(hoveredTower);
                }
                hoveredTower = Instantiate(prefab, location, Quaternion.identity);
                
                SpriteRenderer renderer = hoveredTower.GetComponent<SpriteRenderer>();
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.3f);

                Tower tower = prefab.GetComponent<Tower>();
                tower.SetData(hoveredTowerLevel, true);
            }
            else
            {
                SpriteRenderer renderer = hoveredTower.GetComponent<SpriteRenderer>();
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0f);
            }
        }

        if (hoveredTower)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 location = tilemap.WorldToCell(mousePos);
            location.x += 0.5f;
            location.y += 0.5f;

            hoveredTower.transform.position = location;
        }
    }
}
