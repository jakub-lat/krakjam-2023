using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource source;
    public AudioSource screamSource;
    public AudioClip walk;
    public AudioClip attack;
    public AudioClip death;
    public AudioClip gotHit;
    

    void WalkSound()
    {
        source.PlayOneShot(walk);
    }
    
    void AttackSound()
    {
        source.PlayOneShot(attack);
    }
    
    void DeathSound()
    {
        source.PlayOneShot(death);
        screamSource.Stop();
    }
    
    void HitSound()
    {
        source.PlayOneShot(gotHit);
    }
}
