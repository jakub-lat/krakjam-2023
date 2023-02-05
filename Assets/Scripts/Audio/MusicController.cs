using System;
using System.Collections.Generic;
using System.Linq;
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
                x.loop = true;
                x.Play();
            }

            sources[(int)currentIndex].volume = 1;
            
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, _) => OnSceneChange(scene.name);
            OnSceneChange(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        public void Switch(MusicType index)
        {
            if (index == currentIndex) return;
            
            Debug.Log($"Switching music to {index}");

            foreach (var x in sources.Where((x, i) => i != (int)currentIndex))
            {
                x.volume = 0;
            }
            
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
