using System;
using Audio;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerBehaviour : MonoSingleton<PlayerBehaviour>
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float pulseHealthBoost = 10f;
        [Header("UI")] public Canvas deathUI;
        

        private float _health;
        private Animator anim;


        private bool gotHit = false;
        private bool attacking = false;
        public bool dead = false;

        public float Health
        {
            get => _health;
            set {
                _health = Math.Clamp(value, 0, maxHealth);
                
                Debug.Log($"health: {_health}");
                
                HealthUI.Current.SetAmount(_health / maxHealth);
                
                if (_health == 0)
                {
                    Debug.Log("Player dead");
                    anim.SetTrigger("Death");
                    dead = true;
                    PlayerSounds.Current.Death();
                }
                else
                {
                    anim.SetTrigger("GotHit");
                    gotHit = true;
                    PlayerSounds.Current.GotHit();
                }
            }
        }


        private RootEffect rootEffect;

        protected override void Awake()
        {
            base.Awake();
            _health = maxHealth;
            anim = GetComponent<Animator>();
            rootEffect = GetComponentInChildren<RootEffect>();
            rootEffect.OnPulse.AddListener(OnPulse);
        }

        private void OnDestroy()
        {
            rootEffect.OnPulse.RemoveListener(OnPulse);
        }

        private void Start()
        {
            deathUI.enabled = false;
        }

        public void GotHit(float amount)
        {
            if(dead) return;
            Health -= amount;
        }

        public bool Attack(AttackType type)
        {
            if (gotHit || attacking) return false;
            anim.SetTrigger("Attack");
            PlayerSounds.Current.Weapon(type);
            return true;
        }

        private void OnPulse()
        {
            Health += pulseHealthBoost;
            Debug.Log("onpulse");
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
