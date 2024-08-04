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
    private float _attackCooldown; // seconds
    private float _cooldownTimer = 0f;
    
    private int _attackRadius;
    
    // prefabs
    public GameObject imgProjectile;
    
    // Start is called before the first frame update

    private void Awake()
    {
        imgProjectile = Resources.Load("Prefabs/Projectiles/CommonProjectile") as GameObject;
        _attackCooldown = 0.5f;
        _attackRadius = 5;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer >= _attackCooldown)
        {
            _cooldownTimer = 0f;
            SpawnProjectile(0);
        }
    }

    void SetData(int level)
    {
        _level = level;
    }

    private void SpawnProjectile(int targetMobId)
    {
        GameObject projectile =  Instantiate(imgProjectile, gameObject.transform.position, Quaternion.identity);
        Projectile towerProjectile = projectile.GetComponent<Projectile>();
        towerProjectile.Initialize(id, 10, targetMobId);
    }
}