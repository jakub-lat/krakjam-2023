using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySounds : MonoBehaviour
{
    public AudioSource source;
    public AudioSource screamSource;
    public AudioClip walk;
    public AudioClip attack;
    public AudioClip death;
    public AudioClip gotHit;

    private float bVolume;
    void Randomize()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(bVolume-0.1f, bVolume+0.1f);
        source.panStereo = Random.Range(-0.05f, 0.05f);
    }

    public void Start()
    {
        screamSource.Play();
        bVolume = source.volume;
    }

    void WalkSound()
    {
        Randomize();
        source.PlayOneShot(walk);
    }
    
    void AttackSound()
    {
        Randomize();
        source.PlayOneShot(attack);
    }
    
    void DeathSound()
    {
        source.PlayOneShot(death);
        screamSource.Stop();
    }
    
    void HitSound()
    {
        Randomize();
        source.PlayOneShot(gotHit);
    }
}
