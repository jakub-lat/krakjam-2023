using System;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerBehaviour : MonoSingleton<PlayerBehaviour>
    {
        [SerializeField] private float maxHealth;
        private float health;
        private Animator anim;
        private bool gotHit = false;
        private bool attacking = false;
        private PlayerAttack _playerAttack;

        public float Health => health;

        protected override void Awake()
        {
            base.Awake();
            health = maxHealth;
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            _playerAttack = PlayerAttack.Current;
        }

        public void GotHit(float amount)
        {
            health = Math.Max(0, health - amount);
            HealthUI.Current.SetAmount(health / maxHealth);
            if (health == 0)
            {
                Debug.Log("Player dead");
                anim.SetTrigger("Death");
            }
            else
            {
                anim.SetTrigger("GotHit");
                gotHit = true;
            }
        }

        public bool Attack()
        {
            if (gotHit || attacking) return false;
            anim.SetTrigger("Attack");
            return true;
        }

        public void EndGotHit()
        {
            gotHit = false;
        }
        
        public void EndAttack()
        {
            attacking = false;
            _playerAttack.EndAttack();
        }
    }
}
