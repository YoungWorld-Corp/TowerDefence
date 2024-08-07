using System;
using UnityEngine;

namespace Ingame
{
    public class StunProjectile : Projectile 
    {
        protected CcStatus _ccStatus;
        
        public void Initialize(int ownerTowerID, int damage, int targetID, Vector3 targetPos, CcStatus ccStatus)
        {
            _damage = damage;
            _ownerTowerID = ownerTowerID;
            
            speed = 5f;
            
            _targetMobID = targetID;
            _targetPos = targetPos;
            _ccStatus = ccStatus;
        }
        
        private void Update()
        {
            Vector3 dir = _targetPos - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, _targetPos) <= 0.05f)
            {
                HitTask();
            }
        }

        private new void HitTask()
        {
            //TODO : spawn hit particle
            var mobs = FindObjectsByType<Mob>(FindObjectsSortMode.InstanceID);
            foreach (var mob in mobs)
            {
                if (mob.GetID() == _targetMobID)
                {
                    mob.TakeCc(_ccStatus);
                    mob.TakeDamage(_damage);
                }
            }
            
            Destroy(gameObject);
        }
    }
}