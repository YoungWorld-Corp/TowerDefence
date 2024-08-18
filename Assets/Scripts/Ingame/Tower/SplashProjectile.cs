using System;
using UnityEngine;

namespace Ingame
{
    public class SplashProjectile : Projectile 
    {
        protected int _splashRangeRadius;
        
        public void InitializeSplashProjectile(Tower ownerTower , Mob mob, int damage, Vector3 targetPos, int spalshRadius)
        {
            base.InitializeProjectile(ownerTower, mob, damage, targetPos, 50f);

            _splashRangeRadius = spalshRadius;
        }

        protected override void HitTask()
        {
            var mobs = FindObjectsByType<Mob>(FindObjectsSortMode.InstanceID);
            foreach (var mob in mobs)
            {
                if (Vector3.Distance(mob.transform.position, transform.position) <= _splashRangeRadius)
                {
                    mob.TakeDamage(_damage);
                }
            }
            
            spawnHitParticleWithDestoryReservation();
            Destroy(gameObject);
        }
        protected override void Update()
        {
            base.Update();
        }
    }
}