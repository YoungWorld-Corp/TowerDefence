using System;
using UnityEngine;

namespace Ingame
{
    public class Projectile : MonoBehaviour
    {
        protected int _damage;
        protected int _ownerTowerID;
        
        public float speed = 10f;
        
        protected Vector3 _targetPos; // trace targetMobID if null.
        protected Tower _ownerTower;
        protected Mob _targetMob;

        public GameObject effectParticle;
        private GameObject _effectParticleSpawned;
        
        
        public virtual void InitializeProjectile(Tower ownerTower , Mob mob, int damage, Vector3 targetPos, float in_speed = 10f)
        {
            _ownerTower = ownerTower;
            _targetMob = mob;
            _damage = damage;
            _targetPos = targetPos;
            speed = in_speed;

            //_effectParticle = (GameObject)Resources.Load("Prefabs/Effects/Fire/Hit Fire(Air)");
        }

        protected Vector3 GetTargetPosition()
        {
            return _targetPos;
        }
        
        protected virtual void Update()
        {
            if (!_targetMob)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
                return;
            }

            Vector3 targetPosition = GetTargetPosition(); 
            
            Vector3 dir = targetPosition - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, targetPosition) <= 0.05f)
            {
                HitTask();
            }
        }
        
        protected void spawnHitParticleWithDestoryReservation()
        {
            if (effectParticle == null) return;

            // spawn effectParticle
            Vector3 targetPosition = GetTargetPosition(); 

            _effectParticleSpawned = Instantiate(effectParticle, targetPosition, Quaternion.identity);
            _effectParticleSpawned.SetActive(true);
            
            var ps = _effectParticleSpawned.GetComponent<ParticleSystem>();
            ps.Play(true);

            // reserve to destroy
            Destroy(_effectParticleSpawned, ps.main.duration);
        }

        protected virtual void HitTask()
        {
            //TODO : spawn hit particle
            _targetMob.TakeDamage(_damage);
            
            spawnHitParticleWithDestoryReservation();
        }
    }
}