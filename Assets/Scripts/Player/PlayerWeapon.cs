using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private int _currentWeapon;
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private Transform _camera;
        private Transform _projectileSpawn;
        private GameObject _weaponPrefab;
        private PlayerInventory _inventory;
        private void SwitchWeapon(int newWeapon)
        {
            Destroy(_weaponPrefab);
            _currentWeapon = newWeapon;
            var weapon = _inventory.GetWeapon(_currentWeapon);
            _weaponPrefab = Instantiate(weapon.GetPrefab(), _weaponTransform.position, _weaponTransform.rotation, _weaponTransform);
            foreach (Transform child in _weaponPrefab.transform)
            {
                // Find a child transform with the right tag.
                if (!child.CompareTag("Projectile")) continue;
                _projectileSpawn = child;
                print(_projectileSpawn);
                break;
            }
        }

        private void OnEnable()
        {
            _shootAction.action.Enable();
        }

        private void OnDisable()
        {
            _shootAction.action.Disable();
        }

        private void Awake()
        {
            _shootAction.action.performed += delegate { Shoot(); };
            _inventory = GetComponent<PlayerInventory>();
            SwitchWeapon(0);
        }

        private void Shoot()
        {
            // Spawn the projectile.
            var weapon = _inventory.GetWeapon(_currentWeapon);
            var projectilePrefab = weapon.GetProjectile();
            var projectile = Instantiate(projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            // Point the projectile at the cursor.
            projectile.transform.rotation = _camera.transform.rotation;
            print("done");
        }
    }
}