using UnityEngine;

namespace Character
{
    public class CharacterReload : MonoBehaviour
    {
        protected CharacterInventory Inventory;
        private CharacterWeapon _weapon;
        private CharacterAnimation _animation;

        protected virtual void Awake()
        {
            Inventory = GetComponent<CharacterInventory>();
            _weapon = GetComponent<CharacterWeapon>();
            _animation = GetComponent<CharacterAnimation>();
        }

        public void Reload()
        {
            // Only reload if a weapon is equipped.
            if (!_weapon.GetCurrentWeapon()) return;
            _animation.Reload();
        }
        
        public virtual void ReloadFinish()
        {
            var currentWeapon = _weapon.GetCurrentWeapon();
            Inventory.SetAmmo(currentWeapon.GetMagazineSize());
            _animation.StopReloading();
        }
    }
}