using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomScreams : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioSource source;

    public float minTime = 3f, maxTime = 6f;

    private float timer = 0;

    private void Start()
    {
        timer = Random.Range(minTime, maxTime);
        bVolume = source.volume;
    }

    private float bVolume;
    void Randomize()
    {
        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(bVolume-0.1f, bVolume+0.1f);
        source.panStereo = Random.Range(-0.4f, 0.4f);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = Random.Range(minTime, maxTime);
            Randomize();
            source.PlayOneShot(clips[Random.Range(0,clips.Count)]);
        }
    }
}
