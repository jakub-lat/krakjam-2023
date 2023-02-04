using System;
using System.Collections;
using Codice.CM.Client.Differences.Graphic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using Utils;

namespace Player
{
    public class PlayerParticles : MonoSingleton<PlayerParticles>
    {
        [SerializeField] private Light2DBase light;
        [SerializeField] private float lightYOffset = 5f;
        [SerializeField] private float rootYOffset = 3f;
        [SerializeField] private float animDuration = 2f;

        private void Start()
        {
            var playerPos = PlayerMovement.Current.transform.position;
            light.transform.position = new Vector2(playerPos.x, -100);
            StartCoroutine(Run());
        }

        private void Step()
        {
            var playerPos = PlayerMovement.Current.transform.position;

            transform.localPosition = new Vector2(0, -rootYOffset);
            
            light.transform.localPosition = new Vector2(0, -lightYOffset * 2);
            light.transform.DOLocalMove(new Vector2(0, lightYOffset), animDuration);
        }

        private IEnumerator Run()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                Step();
            }
        }
    }
}
