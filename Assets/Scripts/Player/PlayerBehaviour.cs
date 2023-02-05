using System;
using Audio;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Player
{
    public class PlayerBehaviour : MonoSingleton<PlayerBehaviour>
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float pulseHealthBoost = 10f;

        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip pulseClip;
        
        [Header("UI")] 
        public CanvasGroup deathUI;
        public Image deathBG;
        public ParticleSystem blood;
        public GameObject rootz;
        

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
                
                Debug.Log($"health: {_health}/{maxHealth}");
                
                HealthUI.Current.SetAmount(_health / maxHealth);
                
                if (_health == 0)
                {
                    Debug.Log("Player dead");
                    anim.SetTrigger("Death");
                    dead = true;
                    PlayerSounds.Current.Death();
                    DialoguesController.Current.ClearDialogues();
                    DialoguesController.Current.Next();
                    
                    
                    rootz.SetActive(false);

                    deathBG.DOFade(0, 0);
                    deathBG.DOFade(1, 5);
                    
                    MusicController.Current.Switch(MusicType.Outro);
                }
                else if(_health < prev)
                {
                    anim.SetTrigger("GotHit");
                    gotHit = true;
                    PlayerSounds.Current.GotHit();
                }
                if(value<0) blood.Play();
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
            deathUI.alpha = 0;
            deathUI.gameObject.SetActive(false);
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
            
            if(type == AttackType.Weak) anim.SetTrigger("Attack");
            else anim.SetTrigger("AttackHard");
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
            Debug.Log("End death - fading UI");
            deathUI.gameObject.SetActive(true);
            deathUI.DOFade(1, 0.5f).SetDelay(1f);
        }
        
        public void EndAttack()
        {
            attacking = false;
            WeaponController.CurrentWeapon.EndAttack();
        }
    }
}
