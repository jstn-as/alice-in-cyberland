using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerReload : CharacterReload
    {
        [SerializeField] private InputActionReference _reloadAction;
        [SerializeField] private AmmoDisplay _ammoDisplay;

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

        protected override void Awake()
        {
            base.Awake();
            _reloadAction.action.performed += OnReload;
        }

        public override void ReloadFinish()
        {
            base.ReloadFinish();
            _ammoDisplay.UpdateAmmo(Inventory.GetAmmo());
        }
    }
}