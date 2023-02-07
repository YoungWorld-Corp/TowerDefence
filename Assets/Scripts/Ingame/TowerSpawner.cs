using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject tower1Prefab;
    private bool isSpawningTime;
    private int towerId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTower(Vector3 cellPos)
    {
        cellPos.x += 0.5f;
        cellPos.y += 0.5f;
        Instantiate(tower1Prefab, cellPos, Quaternion.identity);
    }
}
