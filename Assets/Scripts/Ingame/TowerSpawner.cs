using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
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

    public void SpawnTower(Vector3 cellPos, int level)
    {
        cellPos.x += 0.5f;
        cellPos.y += 0.5f;
        
        GameObject towerPrefab;
        towerPrefab = (GameObject)Resources.Load("Prefabs/Towers/Tower" + level.ToString());
        Tower tower = towerPrefab.GetComponent<Tower>();
        tower.SetData(level, false);

        Instantiate(towerPrefab, cellPos, Quaternion.identity);
    }
}
