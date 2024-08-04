using UnityEngine;

namespace Ingame
{
    public class SplashTower : Tower
    {
        protected int _splashRadius = 3;
        
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
            SplashProjectile towerProjectile = projectile.GetComponent<SplashProjectile>();
            
            var targetPos = GameObject.Find("Mob_" + targetMobId).GetComponent<Mob>().transform.position;
            towerProjectile.Initialize(id, damage, _splashRadius, targetPos);
        }
    }
}