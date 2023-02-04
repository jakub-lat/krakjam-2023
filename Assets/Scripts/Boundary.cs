using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour.Current.GotHit(10000000000);
        }
    }
}
