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
            SplashProjectile towerProjectile = projectile.GetComponent<SplashProjectile>();
            
            Vector3 targetPos = targetMob.transform.position;
            towerProjectile.InitializeSplashProjectile(this, targetMob, damage, targetPos, _splashRadius);
        }
    }
}