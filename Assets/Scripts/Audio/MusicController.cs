using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Audio
{
    public enum MusicType
    {
        Ambient,
        Part1,
        Part2,
        Outro
    }
    
    public class MusicController : MonoSingleton<MusicController>
    {
        [SerializeField] private List<AudioSource> sources;

        private MusicType currentIndex;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            foreach (var x in sources)
            {
                x.volume = 0;
                x.Play();
            }
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, _) => OnSceneChange(scene.name);
            OnSceneChange(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void Switch(MusicType index)
        {
            Debug.Log($"Switching music to {index}");
            sources[(int)currentIndex].DOFade(0, 0.5f);
            sources[(int)index].DOFade(1, 0.5f);
            currentIndex = index;
        }

        public void OnSceneChange(string name)
        {
            if (name == "Menu")
            {
                Switch(MusicType.Part1);
            }
            else
            {
                Switch(MusicType.Ambient);
            }
        }
    }
}
