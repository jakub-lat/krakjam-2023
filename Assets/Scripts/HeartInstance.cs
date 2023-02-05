using UnityEngine;
using Utils;

public class HeartInstance : MonoSingleton<HeartInstance>
{
        public AudioClip beatSound;
        public AudioSource source;

        public void PLayBeat()
        {
                source.PlayOneShot(beatSound);
        }

}
