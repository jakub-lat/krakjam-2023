using System;
using UnityEngine;

namespace Audio
{
    public class MusicTrigger : MonoBehaviour
    {
        [SerializeField] private MusicType musicType;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            
            MusicController.Current.Switch(musicType);
        }
    }
}
