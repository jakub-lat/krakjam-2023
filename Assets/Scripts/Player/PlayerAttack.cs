using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public enum AttackType
    {
        Weak,
        Strong
    }

    public class PlayerAttack : MonoSingleton<PlayerAttack>
    {
        [SerializeField] private new Collider2D collider;
        [SerializeField] private LayerMask enemyLayerMask;

        [SerializeField] private float weakAttackDamage = 20f;
        [SerializeField] private float weakAttackCooldown = 0.2f;

        [SerializeField] private float strongAttackDamage = 35f;
        [SerializeField] private float strongAttackCooldown = 0.7f;

        private PlayerBehaviour player;
        private AttackType lastAttackType;

        private void Update()
        {
            if (player.dead) return;
            Attack();
        }

        private void Start()
        {
            player = PlayerBehaviour.Current;
        }


        private float timer = 0;
        private bool attacking = false;

        private void Attack()
        {
            timer -= Time.deltaTime;
            if (timer >= 0) return;

            AttackType? type = Input.GetMouseButtonDown(0)
                ? AttackType.Weak
                : Input.GetMouseButtonDown(1)
                    ? AttackType.Strong
                    : null;

            if (type == null) return;
            var t = type.Value;

            if (!player.Attack(t)) return;

            timer = t == AttackType.Strong ? strongAttackCooldown : weakAttackCooldown;
            attacking = true;
            lastAttackType = t;

            // var res = Physics2D.OverlapBox(transform.position,
            //     new Vector2(collider.bounds.size.x * transform.lossyScale.x,
            //         collider.bounds.size.y * transform.lossyScale.y), transform.rotation.eulerAngles.z,
            //     enemyLayerMask);
            // if (res)
            // {
            //     var enemy = res.GetComponent<Enemy>();
            //     enemy ??= res.GetComponentInParent<Enemy>();
            //     enemy.GotHit(attackDamage);
            // }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (attacking && other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>()
                    .GotHit(lastAttackType == AttackType.Strong ? strongAttackDamage : weakAttackDamage);
                attacking = false;
            }
        }

        public void EndAttack()
        {
            attacking = false;
        }
    }
}
