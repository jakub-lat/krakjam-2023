using System;
using DG.Tweening;
using Player;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Audio
{
    public class PlayerSounds : MonoSingleton<PlayerSounds>
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioSource walkSource;
        
        [SerializeField] private AudioClip walkStep;
        [SerializeField] private AudioClip death;
        [SerializeField] private AudioClip weaponLight;
        [SerializeField] private AudioClip weaponHeavy;
        [SerializeField] private AudioClip gotHit;
        [SerializeField] private AudioClip pickup;
        [SerializeField] private AudioClip jump;
        [SerializeField] private AudioClip land;

        private bool isStopping = false;

        private float bVolume=1;

        private void Start()
        {
            bVolume = source.volume;
        }

        void Randomize()
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            source.volume = Random.Range(bVolume-0.1f, bVolume+0.1f);
            source.panStereo = Random.Range(-0.05f, 0.05f);
        }
        

        public void StartWalking()
        {
            return;
            if (walkSource.isPlaying) return;
            walkSource.volume = 1;
            walkSource.Play();
        }
        
        public void WalkStep()
        {
            Randomize();
            source.PlayOneShot(walkStep);
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
            Randomize();
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

        public void Jump()
        {
            Randomize();
            source.PlayOneShot(jump);
        }

        public void Land()
        {
            Randomize();
            source.PlayOneShot(land);
        }
    }
}
