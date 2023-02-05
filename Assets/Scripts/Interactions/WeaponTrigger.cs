using DefaultNamespace;
using Player;
using UnityEngine;

namespace Interactions
{
    public class WeaponTrigger : BaseInteraction
    {
        [SerializeField] private int targetWeaponIndex;

        protected override string InteractionText =>
            $"Pick up {WeaponController.Current.Weapons[targetWeaponIndex].weaponName}";

        protected override void Interact()
        {
            WeaponController.Current.Switch(targetWeaponIndex);
            Destroy(gameObject);
        }
    }
}
