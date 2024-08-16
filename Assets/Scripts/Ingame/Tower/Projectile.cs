using System;
using CartoonFX;
using UnityEngine;

namespace Ingame
{
    public class Projectile : MonoBehaviour
    {
        protected int _damage;
        protected int _ownerTowerID;
        protected int _targetMobID;
        
        public float speed = 10f;
        
        protected Vector3 _targetPos;
        protected Mob _targetComponent;

        protected GameObject _effectParticle;
        
        
        public void Initialize(int ownerTowerID, int damage, int targetMobID)
        {
            _damage = damage;
            _ownerTowerID = ownerTowerID;
            _targetMobID = targetMobID;

            _targetComponent = GameObject.Find("Mob_" + _targetMobID).GetComponent<Mob>();
            
            _effectParticle = (GameObject)Resources.Load("Prefabs/Effects/Fire/Hit Fire(Air)");
        }
        
        protected void Update()
        {
            if (!_targetComponent)
            {
                Destroy(gameObject);
                return;
            }
            
            _targetPos = GameObject.Find("Mob_" + _targetMobID).transform.position;
            
            Vector3 dir = _targetPos - transform.position;
            transform.Translate(dir.normalized * (speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, _targetPos) <= 0.05f)
            {
                HitTask();
            }
        }
        
        protected float spawnHitParticle()
        {
            
            // spawn effectParticle
            Instantiate(_effectParticle, _targetPos, Quaternion.identity);
            _effectParticle.SetActive(true);
            
            var ps = _effectParticle.GetComponent<ParticleSystem>();
            ps.Play(true);

            // return particle duration
            return ps.main.duration;
        }

        protected void HitTask()
        {
            //TODO : spawn hit particle
            _targetComponent.TakeDamage(_damage);
            
            var duration = spawnHitParticle();
            Destroy(_effectParticle, duration);
        }
    }
}