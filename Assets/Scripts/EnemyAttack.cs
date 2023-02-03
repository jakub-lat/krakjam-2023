using System;
using System.Collections;
using System.Collections.Generic;
using Player;
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
        PlayerBehaviour.Current.GotHit();
        attacking = false;
        // sound or particles
        Debug.Log("Player hit!");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(attacking) Hit();   
        }
    }
}
