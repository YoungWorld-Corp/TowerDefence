using System;
using UnityEngine;

namespace Ingame
{
    public class StunProjectile : Projectile 
    {
        protected CcStatus _ccStatus;
        
        public void InitializeStunProjectile(Tower ownerTower , Mob mob, int damage, Vector3 targetPos, CcStatus ccStatus)
        {
            base.InitializeProjectile(ownerTower, mob, damage, targetPos, 5f);
            
            _ccStatus = ccStatus;
        }

        protected override void HitTask()
        {
            _targetMob.TakeCc(_ccStatus);
            _targetMob.TakeDamage(_damage);
           
            SpawnHitParticleWithDestroyReservation();
            Destroy(gameObject);
        }
        protected override void Update()
        {
            base.Update();
        }
    }
}