using Cam;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private InputActionReference _shootAction;
        [SerializeField] private InputActionReference _reloadAction;
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private Transform _camera;
        [SerializeField] private CameraRotate _cameraRotate;
        [SerializeField] private WeaponIcon _weaponIcon;
        [SerializeField] private AmmoDisplay _ammoDisplay;
        [SerializeField] private CrosshairDisplay _crosshairDisplay;
        private Transform _projectileSpawn;
        private GameObject _weaponPrefab;
        private PlayerInventory _inventory;
        private float _timeSinceShot;
        private bool _shootPressed;
        private void SwitchWeapon(int newWeapon)
        {
            Destroy(_weaponPrefab);
            _inventory.SetCurrentWeapon(newWeapon);
            var weapon = _inventory.GetCurrentWeapon();
            _weaponIcon.SetSprite(weapon.GetWeaponIcon());
            _weaponPrefab = Instantiate(weapon.GetPrefab(), _weaponTransform.position, _weaponTransform.rotation, _weaponTransform);
            _inventory.SetAmmo(weapon.GetMagazineSize());
            _ammoDisplay.UpdateAmmo(_inventory.GetAmmo());
            _ammoDisplay.UpdateMaxAmmo(weapon.GetMagazineSize());
            _crosshairDisplay.SetCrosshair(weapon.GetCrosshair());
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
            var weapon = _inventory.GetCurrentWeapon();
            if (_inventory.GetAmmo() <= 0) return;
            if (_timeSinceShot < weapon.GetFireRate()) return;
            _timeSinceShot = 0;
            _inventory.ConsumeAmmo();
            _ammoDisplay.UpdateAmmo(_inventory.GetAmmo());
            // Add recoil.
            var recoilRange = weapon.GetRecoil();
            var recoilX = Random.Range(-recoilRange.x, recoilRange.x);
            var recoilY = Random.Range(0, recoilRange.y);
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