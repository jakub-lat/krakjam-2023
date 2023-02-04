using DG.Tweening;
using Player;
using UnityEngine;
using Utils;

namespace Audio
{
    public class PlayerSounds : MonoSingleton<PlayerSounds>
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioSource walkSource;

        [SerializeField] private AudioClip death;
        [SerializeField] private AudioClip weaponLight;
        [SerializeField] private AudioClip weaponHeavy;
        [SerializeField] private AudioClip gotHit;
        [SerializeField] private AudioClip pickup;

        private bool isStopping = false;

        public void StartWalking()
        {
            if (walkSource.isPlaying) return;
            walkSource.volume = 1;
            walkSource.Play();
        }

        public void StopWalking()
        {
            if (isStopping) return;
            isStopping = true;
            walkSource.DOFade(0, 0.5f).OnComplete(() =>
            {
                walkSource.Stop();
            });
        }

        public void Death()
        {
            source.PlayOneShot(death);
        }

        public void Weapon(AttackType type)
        {
            source.PlayOneShot(type == AttackType.Strong ? weaponHeavy : weaponLight);
        }

        public void GotHit()
        {
            source.PlayOneShot(gotHit);
        }
        
        public void Pickup()
        {
            source.PlayOneShot(pickup);
        }
    }
}
