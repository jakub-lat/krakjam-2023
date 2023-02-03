using System;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
    public class PlayerBehaviour : MonoSingleton<PlayerBehaviour>
    {
        [SerializeField] private float maxHealth;
        private float health;

        public float Health => health;

        protected override void Awake()
        {
            base.Awake();
            health = maxHealth;
        }

        public void GotHit(float amount)
        {
            health = Math.Max(0, health - amount);
            HealthUI.Current.SetAmount(health / maxHealth);
            if (health == 0)
            {
                Debug.Log("Player dead");
            }
        }
    }
}
