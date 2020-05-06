using Cam;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerAnimation : CharacterAnimation
    {
        [SerializeField] private Collider _standingCollider;
        [SerializeField] private Collider _crouchingCollider;
        [SerializeField] private InputActionReference _aimAction;
        [SerializeField] private FovManager _fovManager;

        private void OnEnable()
        {
            _aimAction.action.Enable();
        }
        private void OnDisable()
        {
            _aimAction.action.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            _aimAction.action.performed += OnAim;
            _aimAction.action.canceled += OnAim;
        }
        private void OnAim(InputAction.CallbackContext obj)
        {
            SetAim(obj.performed);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _crouchingCollider.enabled = IsCrouching;
            _standingCollider.enabled = !IsCrouching;
        }

        protected override void LateUpdate()
        {
            _fovManager.SetAiming(IsAiming);
            base.LateUpdate();
        }
    }
}