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
    private bool _bDisplayMode = false;
    
    // prefabs
    public GameObject imgProjectile;
    
    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {
        imgProjectile = Resources.Load("Prefabs/Projectiles/CommonProjectile") as GameObject;
        Debug.Log(imgProjectile);
        
        _attackCooldown = 0.5f;
        _attackRadius = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bDisplayMode) return;

        var Mobs = FindObjectsByType<Mob>(FindObjectsSortMode.InstanceID);
        foreach (var mob in Mobs)
        {
            // 사거리 체크
            if (Vector3.Distance(mob.transform.position, transform.position) <= _attackRadius)
            {
                _cooldownTimer += Time.deltaTime;
                if (_cooldownTimer >= _attackCooldown)
                {
                    _cooldownTimer = 0f;
                    SpawnProjectile(mob.GetID());

                    break;
                }
            }
        }
    }

    public void SetData(int level, bool bDisplayMode)
    {
        _level = level;
        _bDisplayMode = bDisplayMode;
    }

    private void SpawnProjectile(int targetMobId)
    {
        GameObject projectile = Instantiate(imgProjectile, gameObject.transform.position, Quaternion.identity);
        Projectile towerProjectile = projectile.GetComponent<Projectile>();
        towerProjectile.Initialize(id, 30, targetMobId);
    }
}