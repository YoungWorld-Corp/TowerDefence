using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingame;

public class Tower : MonoBehaviour
{
    int id;
    private float _attackSpeed;
    private int _level;
    
    // prefabs
    public GameObject prefabProjectile;
    
    // Start is called before the first frame update

    private void Awake()
    {
        prefabProjectile = Resources.Load("Prefabs/") as GameObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetData(int level)
    {
        _level = level;
    }

    public void SpawnProjectile(List<Vector3> wayPointsWorld)
    {
        GameObject projectile =  Instantiate(prefabProjectile, gameObject.transform.position, Quaternion.identity);
        Projectile towerProjectile = projectile.GetComponent<Projectile>() as Projectile;
        towerProjectile.Initialize(id, 10);
    }
}