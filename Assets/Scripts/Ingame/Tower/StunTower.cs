using UnityEngine;

namespace Ingame
{
    public class StunTower : Tower
    {
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
        
        protected new void SpawnProjectile(int targetMobId, int damage)
        {
            GameObject projectile = Instantiate(imgProjectile, gameObject.transform.position, Quaternion.identity);
            StunProjectile towerProjectile = projectile.GetComponent<StunProjectile>();
            
            var targetPos = GameObject.Find("Mob_" + targetMobId).GetComponent<Mob>().transform.position;
            
            CcStatus ccStatus = new CcStatus();
            ccStatus.stunDuration = 0.3f;
            
            towerProjectile.Initialize(id, damage, targetMobId, targetPos, ccStatus);
        }
    }
}