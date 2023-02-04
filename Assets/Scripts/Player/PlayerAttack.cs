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
        [SerializeField] private float cooldown = 1f;
        private PlayerBehaviour player;

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
            if (!Input.GetMouseButtonDown(0) || timer>=0) return;
            if (!player.Attack()) return;

            timer = cooldown;
            attacking = true;

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
                other.GetComponent<Enemy>().GotHit(attackDamage);
                attacking = false;
            }
        }

        public void EndAttack()
        {
            attacking = false;
        }
    }
}
