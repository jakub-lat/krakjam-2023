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

        private void Start()
        {
            var playerPos = PlayerMovement.Current.transform.position;
            light.transform.position = new Vector2(playerPos.x, -100);
            StartCoroutine(Run());
        }

        private void Step()
        {
            var playerPos = PlayerMovement.Current.transform.position;

            transform.position = new Vector2(playerPos.x, playerPos.y - rootYOffset);
            
            light.transform.position = new Vector2(playerPos.x, playerPos.y - lightYOffset * 2);
            light.transform.DOMove(new Vector2(playerPos.x, playerPos.y + lightYOffset), 1f);
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
