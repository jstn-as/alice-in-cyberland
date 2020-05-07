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
        private CharacterWeapon _characterWeapon;
        private CharacterReload _characterReload;

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
            _characterWeapon = GetComponent<PlayerWeapon>();
            _characterReload = GetComponent<CharacterReload>();
        }

        public bool AddWeapon(WeaponObject weapon)
        {
            // for (var i = 0; i < _weapons.Count; i++)
            // {
            //     if (_weapons[i] != null) continue;
            //     _weapons[i] = weapon;
            //     _playerWeapon.SwitchWeapon(i);
            //     _characterReload.Reload();
            //     return true;
            // }
            //
            // return false;
            _weapons[0] = weapon;
            _characterWeapon.SwitchWeapon(0);
            _characterReload.Reload();
            return true;
        }
    }
}