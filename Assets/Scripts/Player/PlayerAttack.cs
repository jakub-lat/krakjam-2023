using System;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerAttack : MonoSingleton<PlayerAttack>
    {
        [SerializeField] private float attackDamage = 20f;
        [SerializeField] private new Collider2D collider;
        [SerializeField] private LayerMask enemyLayerMask;

        private Collider2D[] results = { };

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            results = new Collider2D[] { };
            var res = Physics2D.OverlapBox(transform.position,
                new Vector2(collider.bounds.size.x * transform.lossyScale.x,
                    collider.bounds.size.y * transform.lossyScale.y), transform.rotation.eulerAngles.z,
                enemyLayerMask);
            if (res)
            {
                var enemy = res.GetComponent<Enemy>();
                enemy ??= res.GetComponentInParent<Enemy>();
                enemy.GotHit(attackDamage);
            }
        }
    }
}
