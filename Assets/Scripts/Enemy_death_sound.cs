using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_death_sound : MonoBehaviour
{
    public AudioSource enemy_death;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeathSound()
    {
        enemy_death.Play();
    }
}