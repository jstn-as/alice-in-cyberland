using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private int _currentWeapon;
        [SerializeField] private WeaponObject _weapon0, _weapon1;
        [SerializeField] private int _weaponAmmo;

        public void SetCurrentWeapon(int currentWeapon)
        {
            _currentWeapon = currentWeapon;
        }
        
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
        public WeaponObject GetCurrentWeapon()
        {
            return _currentWeapon == 0 ? _weapon0 : _weapon1;
        }
    }
}