using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private WeaponObject _weapon0, _weapon1;
        [SerializeField] private int _weaponAmmo;
        
        public int GetAmmo()
        {
            return _weaponAmmo;
        }

        public void ConsumeAmmo()
        {
            _weaponAmmo -= 1;
        }

        public void SetAmmo(int newAmmo)
        {
            _weaponAmmo = newAmmo;
        }
        public WeaponObject GetWeapon(int slot)
        {
            return slot == 0 ? _weapon0 : _weapon1;
        }
    }
}