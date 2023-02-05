using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public class WeaponController : MonoSingleton<WeaponController>
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public List<Weapon> Weapons { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Weapons = GetComponents<Weapon>().OrderBy(x => x.weaponIndex).ToList();
        }

        private void Start()
        {
            Switch(0);
        }

        public static Weapon CurrentWeapon { get; private set; }

        public void Switch(int index)
        {
            foreach (var x in Weapons)
            {
                x.enabled = false;
            }

            CurrentWeapon = Weapons[index];
            CurrentWeapon.enabled = true;
            spriteRenderer.sprite = CurrentWeapon.sprite;
            PlayerSounds.Current.Pickup();
        }

        public void Switch(string weaponName) => Switch(Weapons.FindIndex(x => x.weaponName == weaponName));

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Switch(1);
            }
        }
    }
}
