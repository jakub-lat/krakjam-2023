using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Player
{
    public class WeaponController : MonoSingleton<WeaponController>
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private List<Weapon> weapons;

        protected override void Awake()
        {
            base.Awake();
            weapons = GetComponents<Weapon>().OrderBy(x => x.weaponIndex).ToList();
        }

        private void Start()
        {
            Switch(0);
        }

        public static Weapon CurrentWeapon { get; private set; }

        public void Switch(int index)
        {
            foreach (var x in weapons)
            {
                x.enabled = false;
            }

            CurrentWeapon = weapons[index];
            CurrentWeapon.enabled = true;
            spriteRenderer.sprite = CurrentWeapon.sprite;
        }

        public void Switch(string weaponName) => Switch(weapons.FindIndex(x => x.weaponName == weaponName));

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Switch(1);
            }
        }
    }
}
