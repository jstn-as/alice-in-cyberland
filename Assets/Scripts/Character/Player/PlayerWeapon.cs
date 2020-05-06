using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerWeapon :CharacterWeapon
    {
        [SerializeField] private InputActionReference _attackAction;
        [SerializeField] private InputActionReference _switchAction;
        [SerializeField] private WeaponIcon _weaponIcon;
        [SerializeField] private AmmoDisplay _ammoDisplay;
        [SerializeField] private CrosshairDisplay _crosshairDisplay;
        [SerializeField] private Transform _camera;
        private void OnEnable()
        {
            _attackAction.action.Enable();
        }

        private void OnDisable()
        {
            _attackAction.action.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            _attackAction.action.performed += OnAttack;
            _attackAction.action.canceled += OnAttack;
        }

        private void OnAttack(InputAction.CallbackContext obj)
        {
            IsAttacking = obj.performed;
        }

        protected override void Shoot()
        {
            // Point the projectile at the cursor.
            var forward = _camera.forward;
            var position = _camera.position;
            var ray = new Ray(position, forward);
            TargetPoint = position + forward * 100;
            if (Physics.Raycast(ray, out var hit, 100f))
            {
                TargetPoint = hit.point;
            }
            base.Shoot();
        }
        public override bool SwitchWeapon(int newWeaponIndex)
        {
            if (base.SwitchWeapon(newWeaponIndex))
            {
                // Update the UI.
                _ammoDisplay.SetVisible(true);
                _ammoDisplay.UpdateMaxAmmo(CurrentWeapon.GetMagazineSize());
                _crosshairDisplay.SetCrosshair(CurrentWeapon.GetCrosshair());
                _weaponIcon.SetSprite(CurrentWeapon.GetWeaponIcon());
                return true;
            }

            // Reset the UI.
            _weaponIcon.SetVisible(false);
            _ammoDisplay.SetVisible(false);
            _crosshairDisplay.ResetCrosshair();
            return false;
        }
    }
}