using UnityEngine;

namespace Ingame
{
    public class StunTower : Tower
    {
        void Update()
        {
            if (_bDisplayMode) return;

            var Mobs = FindObjectsByType<Mob>(FindObjectsSortMode.InstanceID);
            foreach (Mob mob in Mobs)
            {
                // 사거리 체크
                if (Vector3.Distance(mob.transform.position, transform.position) <= _attackRadius)
                {
                    _cooldownTimer += Time.deltaTime;
                    if (_cooldownTimer >= _attackCooldown)
                    {
                        _cooldownTimer = 0f;
                        SpawnProjectile(mob, RandomizeDamage(_damage));

                        break;
                    }
                }
            }
        }
        
        protected new void SpawnProjectile(Mob targetMob, int damage)
        {
            GameObject projectile = Instantiate(imgProjectile, gameObject.transform.position, Quaternion.identity);
            projectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            StunProjectile towerProjectile = projectile.GetComponent<StunProjectile>();
            
            var targetPos = targetMob.transform.position;
            
            CcStatus ccStatus = new CcStatus();
            ccStatus.stunDuration = 0.3f;
            
            if (towerProjectile == null)
            {
                Debug.Log("towerProject null");
            }
            towerProjectile.InitializeStunProjectile(this, targetMob, damage, targetPos, ccStatus);
        }
    }
}