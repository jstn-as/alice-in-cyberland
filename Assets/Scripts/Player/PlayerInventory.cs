using UnityEngine;
using Weapon;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private WeaponObject _weapon0, _weapon1;
        [SerializeField] private int _pistolAmmo, _uziAmmo, _sniperAmmo;

        public WeaponObject GetWeapon(int slot)
        {
            return slot == 0 ? _weapon0 : _weapon1;
        }
    }
}