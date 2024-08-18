using System;
using UnityEngine;

namespace Ingame
{
    public class SlowProjectile : Projectile 
    {
        protected int _splashRangeRadius;
        protected CcStatus _ccStatus;
        
        public void InitializeSlowProjectile(Tower ownerTower, Mob mob, int damage, Vector3 targetPos, int splashRadius, CcStatus ccStatus)
        {
            base.InitializeProjectile(ownerTower, mob, damage, targetPos, 50f);

            _splashRangeRadius = splashRadius;
            _ccStatus = ccStatus;
        }

        protected override void HitTask()
        {
            //TODO : spawn hit particle
            var mobs = FindObjectsByType<Mob>(FindObjectsSortMode.InstanceID);
            foreach (var mob in mobs)
            {
                if (Vector3.Distance(mob.transform.position, transform.position) <= _splashRangeRadius)
                {
                    mob.TakeCc(_ccStatus);
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