using Character.Player;
using UnityEngine;
using Weapon;

namespace Character
{
    public class CharacterWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _weaponTransform;
        protected Vector3 TargetPoint;
        protected WeaponObject CurrentWeapon;
        protected bool IsAttacking;
        private CharacterAnimation _characterAnimation;
        private Transform _projectileSpawn;
        private GameObject _weaponPrefab;
        private CharacterInventory _inventory;
        private PlayerReload _playerReload;
        private float _timeSinceShot;

        public WeaponObject GetCurrentWeapon()
        {
            return CurrentWeapon;
        }
        protected virtual void Awake()
        {
            _playerReload = GetComponent<PlayerReload>();
            _inventory = GetComponent<CharacterInventory>();
            _characterAnimation = GetComponent<CharacterAnimation>();
        }
        private void Start()
        {
            SwitchWeapon(0);
            if (CurrentWeapon)
            {
                _playerReload.ReloadFinish();
            }
            enabled = false;
        }
        protected virtual void Update()
        {
            _timeSinceShot += Time.deltaTime;
            // Skip if not attacking.
            if (!IsAttacking) return;
            if (CurrentWeapon)
            {
                Shoot();
            }
            else
            {
                Punch();
            }
        }
        private void Punch()
        {
            if (!_characterAnimation.IsPunching())
            {
                _characterAnimation.Punch();
            }
        }
        protected virtual void Shoot()
        {
            // Don't shoot if reloading.
            if (_characterAnimation.IsReloading()) return;
            // Don't shoot if out of ammo.
            if (_inventory.GetAmmo() <= 0) return;
            if (_timeSinceShot < CurrentWeapon.GetFireRate()) return;
            _timeSinceShot = 0;
            _inventory.ConsumeAmmo();
            // // Add recoil.
            // var recoilRange = weapon.GetRecoil();
            // var recoilX = Random.Range(-recoilRange.x, recoilRange.x);
            // var recoilY = Random.Range(0, recoilRange.y);
            // var recoil = new Vector2(recoilX, recoilY);
            // _cameraRotate.AddRecoil(recoil);
            // Spawn the projectile.
            var projectilePrefab = CurrentWeapon.GetProjectile();
            var projectile = Instantiate(projectilePrefab, _projectileSpawn.position, Quaternion.identity);
            // Point the projectile at the cursor.
            projectile.transform.LookAt(TargetPoint);
        }
        public virtual bool SwitchWeapon(int newWeaponIndex)
        {
            Destroy(_weaponPrefab);
            CurrentWeapon = _inventory.GetWeapon(newWeaponIndex);
            // Case if no weapon is equipped.
            if (!CurrentWeapon)
                return false;
            _weaponPrefab = Instantiate(CurrentWeapon.GetPrefab(), _weaponTransform.position, _weaponTransform.rotation, _weaponTransform);
            // Find the transform that marks the spawn of the projectile.
            foreach (Transform child in _weaponPrefab.transform)
            {
                // Find a child transform with the right tag.
                if (!child.CompareTag("Projectile")) continue;
                _projectileSpawn = child;
                break;
            }

            return true;
        }
    }
}