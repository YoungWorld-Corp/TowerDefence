using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ingame;

public class Tower : MonoBehaviour
{
    int id;
    private int _level;
    private int _damage;
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
                    SpawnProjectile(mob.GetID(), _damage);

                    break;
                }
            }
        }
    }

    public void SetData(int level, bool bDisplayMode)
    {
        _level = level;
        // int to enum
        _damage = (int)Meta.TowerDamageFromLevel(level);
        _attackCooldown = Meta.TowerAttackCooldownFromLevel(level);
        _attackRadius = Meta.TowerAttackRadisFromLevel(level);
        _bDisplayMode = bDisplayMode;
    }

    private void SpawnProjectile(int targetMobId, int damage)
    {
        GameObject projectile = Instantiate(imgProjectile, gameObject.transform.position, Quaternion.identity);
        Projectile towerProjectile = projectile.GetComponent<Projectile>();
        towerProjectile.Initialize(id, damage, targetMobId);
    }
}