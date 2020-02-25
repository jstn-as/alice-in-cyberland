using System;
using Cam;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private int _currentWeapon;
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private Transform _camera;
        [SerializeField] private CameraRotate _cameraRotate;
        [SerializeField] private WeaponIcon _weaponIcon;
        private Transform _projectileSpawn;
        private GameObject _weaponPrefab;
        private PlayerInventory _inventory;
        private float _timeSinceShot;
        private bool _shootPressed;
        private void SwitchWeapon(int newWeapon)
        {
            Destroy(_weaponPrefab);
            _currentWeapon = newWeapon;
            var weapon = _inventory.GetWeapon(_currentWeapon);
            _weaponIcon.SetSprite(weapon.GetSprite());
            _weaponPrefab = Instantiate(weapon.GetPrefab(), _weaponTransform.position, _weaponTransform.rotation, _weaponTransform);
            foreach (Transform child in _weaponPrefab.transform)
            {
                // Find a child transform with the right tag.
                if (!child.CompareTag("Projectile")) continue;
                _projectileSpawn = child;
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
            _shootAction.action.performed += delegate { _shootPressed = true; };
            _shootAction.action.canceled += delegate { _shootPressed = false; };
            _inventory = GetComponent<PlayerInventory>();
        }

        private void Start()
        {
            SwitchWeapon(0);
        }

        private void Update()
        {
            _timeSinceShot += Time.deltaTime;
            if (_shootPressed)
            {
                Shoot();
            }
        }
        private void Shoot()
        {
            var weapon = _inventory.GetWeapon(_currentWeapon);
            if (_timeSinceShot < weapon.GetFireRate()) return;
            _timeSinceShot = 0;
            // Add recoil.
            var recoilRange = weapon.GetRecoil();
            var recoilX = Random.Range(-recoilRange.x, recoilRange.x);
            var recoilY = Random.Range(-recoilRange.y, recoilRange.y);
            var recoil = new Vector2(recoilX, recoilY);
            _cameraRotate.AddRecoil(recoil);
            // Spawn the projectile.
            var projectilePrefab = weapon.GetProjectile();
            var projectile = Instantiate(projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            // Point the projectile at the cursor.
            var forward = _camera.forward;
            var position = _camera.position;
            var ray = new Ray(position, forward);
            var targetPoint = position + forward * 100;
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                targetPoint = hit.point;
            }
            projectile.transform.LookAt(targetPoint);
        }
    }
}