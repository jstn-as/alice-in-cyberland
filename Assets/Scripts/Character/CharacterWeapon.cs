using UnityEngine;
using Weapon;

namespace Character
{
    public class CharacterWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private AudioClip _attackSound;
        protected Vector3 TargetPoint;
        protected WeaponObject CurrentWeapon;
        protected CharacterInventory Inventory;
        protected CharacterAnimation CharacterAnimation;
        protected bool IsAttacking;
        private float _timeSinceShot;
        private Transform _projectileSpawn;
        private GameObject _weaponPrefab;
        private CharacterReload _characterReload;
        private AudioSource _audioSource;

        public WeaponObject GetCurrentWeapon()
        {
            return CurrentWeapon;
        }
        protected virtual void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _characterReload = GetComponent<CharacterReload>();
            Inventory = GetComponent<CharacterInventory>();
            CharacterAnimation = GetComponent<CharacterAnimation>();
        }
        protected virtual void Start()
        {
            SwitchWeapon(0);
            if (CurrentWeapon)
            {
                _characterReload.ReloadFinish();
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
            // Skip if already punching.
            if (CharacterAnimation.IsPunching())
                return;
            _audioSource.PlayOneShot(_attackSound);
            CharacterAnimation.Punch();
        }
        protected virtual void Shoot()
        {
            // Don't shoot if reloading.
            if (CharacterAnimation.IsReloading())
                return;
            // Don't shoot if out of ammo.
            if (Inventory.GetAmmo() <= 0)
                return;
            if (_timeSinceShot < CurrentWeapon.GetFireRate())
                return;
            _audioSource.PlayOneShot(_attackSound);
            _timeSinceShot = 0;
            Inventory.ConsumeAmmo();
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
            CurrentWeapon = Inventory.GetWeapon(newWeaponIndex);
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