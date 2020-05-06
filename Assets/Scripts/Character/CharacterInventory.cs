using System.Collections.Generic;
using Character.Player;
using UnityEngine;
using Weapon;

namespace Character
{
    public class CharacterInventory : MonoBehaviour
    {
        [SerializeField] private int _weaponAmmo;
        [SerializeField] private List<WeaponObject> _weapons;
        private PlayerWeapon _playerWeapon;
        private PlayerReload _playerReload;

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

        public WeaponObject GetWeapon(int weaponIndex)
        {
            return _weapons[weaponIndex];
        }

        private void Awake()
        {
            _playerWeapon = GetComponent<PlayerWeapon>();
            _playerReload = GetComponent<PlayerReload>();
        }

        public bool AddWeapon(WeaponObject weapon)
        {
            for (var i = 0; i < _weapons.Count; i++)
            {
                if (_weapons[i] != null) continue;
                _weapons[i] = weapon;
                _playerWeapon.SwitchWeapon(i);
                _playerReload.Reload();
                return true;
            }

            return false;
        }
    }
}