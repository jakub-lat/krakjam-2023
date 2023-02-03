using System;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerAttack : MonoSingleton<PlayerAttack>
    {
        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            
        }
    }
}
