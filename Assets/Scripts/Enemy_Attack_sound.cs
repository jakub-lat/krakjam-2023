using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_sound : MonoBehaviour
{
    public AudioSource enemy_attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AttackSound()
    {
        enemy_attack.Play();
    }
}