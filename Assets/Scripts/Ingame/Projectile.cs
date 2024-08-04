using System;
using UnityEngine;

namespace Ingame
{
    public class Projectile : MonoBehaviour
    {
        private int _damage = 30;
        private int _ownerTowerID;
        
        
        public void Initialize(int ownerTowerID, int damage)
        {
            _damage = damage;
            _ownerTowerID = ownerTowerID;
        }


        private void Update()
        {
            
        }
    }
}