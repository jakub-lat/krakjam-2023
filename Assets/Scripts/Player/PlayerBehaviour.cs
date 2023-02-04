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
        public bool dead = false;

        public float Health => health;

        [Header("UI")] public Canvas deathUI;

        protected override void Awake()
        {
            base.Awake();
            health = maxHealth;
            anim = GetComponent<Animator>();
        }

        private void Start()
        {
            deathUI.enabled = false;
        }

        public void GotHit(float amount)
        {
            if(dead) return;
            health = Math.Max(0, health - amount);
            HealthUI.Current.SetAmount(health / maxHealth);
            if (health == 0)
            {
                Debug.Log("Player dead");
                anim.SetTrigger("Death");
                dead = true;
            }
            else
            {
                anim.SetTrigger("GotHit");
                gotHit = true;
            }
        }

        public bool Attack(AttackType type)
        {
            if (gotHit || attacking) return false;
            anim.SetTrigger("Attack");
            return true;
        }

        public void EndGotHit()
        {
            gotHit = false;
        }

        public void EndDeath()
        {
            deathUI.enabled = true;
        }
        
        public void EndAttack()
        {
            attacking = false;
            WeaponController.CurrentWeapon.EndAttack();
        }
    }
}
