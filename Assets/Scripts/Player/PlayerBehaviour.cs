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

        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip pulseClip;
        
        [Header("UI")] public Canvas deathUI;
        public ParticleSystem blood;
        

        private float _health;
        private Animator anim;


        private bool gotHit = false;
        private bool attacking = false;
        public bool dead = false;

        public float Health
        {
            get => _health;
            set
            {
                var prev = _health;
                _health = Math.Clamp(value, 0, maxHealth);
                
                // Debug.Log($"health: {_health}");
                
                HealthUI.Current.SetAmount(_health / maxHealth);
                
                if (_health == 0)
                {
                    Debug.Log("Player dead");
                    anim.SetTrigger("Death");
                    dead = true;
                    PlayerSounds.Current.Death();
                }
                else if(_health < prev)
                {
                    anim.SetTrigger("GotHit");
                    gotHit = true;
                    PlayerSounds.Current.GotHit();
                }
                blood.Play();
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

        private AttackType c;
        public bool Attack(AttackType type)
        {
            if (gotHit || attacking) return false;
            c = type;
            anim.SetTrigger("Attack");
            return true;
        }

        public void RightMomentForAttackSound()
        {
            PlayerSounds.Current.Weapon(c);
        }

        private void OnPulse()
        {
            Health += pulseHealthBoost;
            source.PlayOneShot(pulseClip);
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
