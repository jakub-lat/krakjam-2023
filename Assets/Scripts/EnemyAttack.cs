using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool attacking = false;

    public void Attack(bool t)
    {
        attacking = t;
    }

    void Hit()
    {
        Character.instance.GotHit();
        attacking = false;
        // sound or particles
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(attacking) Hit();   
        }
    }
}
