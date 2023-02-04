using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool attacking = false;
    public float dmg = 20;

    public void Attack(bool t)
    {
        attacking = t;
    }

    void Hit()
    {
        PlayerBehaviour.Current.GotHit(dmg);
        attacking = false;
        // sound or particles
        // Debug.Log("Player hit!");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(attacking) Hit();   
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            if(attacking) Hit();   
        }
    }
}
