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
    private int _hoveredTowerLevel = -1;
    private DeckMade _hoveredTowerDeckMade;

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
            
            if (GameState.Instance.nextDeckMade.deckMadeType < 0) return;

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int c0 = tilemap.WorldToCell(mouseWorldPos);

            towerSpawner.SpawnTower(c0, GameState.Instance.nextDeckMade);
            GameState.Instance.nextDeckMade = new DeckMade(DeckMadeType.None, -1);
            
            // ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            // {
            //     if (hit.transform.CompareTag("Tile"))
            //     {
            //         towerSpawner.SpawnTower(hit.transform);
            //     }
            // }
        }
        
        int nextTowerLevel = (int)GameState.Instance.nextDeckMade.deckMadeType;
        if (_hoveredTowerLevel != nextTowerLevel)
        {
            // Change sprite
            _hoveredTowerLevel = nextTowerLevel;
            _hoveredTowerDeckMade = GameState.Instance.nextDeckMade;
            if (_hoveredTowerLevel >= 0)
            {
                var prefab = (GameObject)Resources.Load("Prefabs/Towers/Tower" + _hoveredTowerLevel.ToString());
                
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
                tower.SetData(_hoveredTowerDeckMade, true);
            }
            else
            {
                if (hoveredTower)
                {
                    Destroy(hoveredTower);
                }
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
