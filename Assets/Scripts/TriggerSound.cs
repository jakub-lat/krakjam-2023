using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource src;
    private bool played = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (played) return;
        if (col.CompareTag("Player"))
        {
            played = true;
            src.Play();
        }
    }
}
