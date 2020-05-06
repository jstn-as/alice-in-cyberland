using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerReload : MonoBehaviour
    {
        [SerializeField] private InputActionReference _reloadAction;
        [SerializeField] private AmmoDisplay _ammoDisplay;
        private CharacterInventory _inventory;
        private PlayerWeapon _playerWeapon;
        private PlayerAnimation _playerAnimation;

        private void OnEnable()
        {
            _reloadAction.action.Enable();
        }

        private void OnDisable()
        {
            _reloadAction.action.Disable();
        }

        private void OnReload(InputAction.CallbackContext obj)
        {
            Reload();
        }

        private void Awake()
        {
            _inventory = GetComponent<CharacterInventory>();
            _playerWeapon = GetComponent<PlayerWeapon>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _reloadAction.action.performed += OnReload;
        }

        public void ReloadFinish()
        {
            var currentWeapon = _playerWeapon.GetCurrentWeapon();
            _inventory.SetAmmo(currentWeapon.GetMagazineSize());
            _ammoDisplay.UpdateAmmo(_inventory.GetAmmo());
            _playerAnimation.ReloadFinish();
        }
        
        public void Reload()
        {
            // Only reload if a weapon is equipped.
            if (!_playerWeapon.GetCurrentWeapon()) return;
            _playerAnimation.Reload();
        }
    }
}