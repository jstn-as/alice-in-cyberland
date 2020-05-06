using Cam;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerMovement : CharacterMovement
    {
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;
        [SerializeField] private InputActionReference _sprintAction;
        [SerializeField] private InputActionReference _crouchAction;
        [SerializeField] private FovManager _fovManager;

        private void OnEnable()
        {
            _moveAction.action.Enable();
            _jumpAction.action.Enable();
            _sprintAction.action.Enable();
            _crouchAction.action.Enable();
        }

        private void OnDisable()
        {
            _moveAction.action.Disable();
            _jumpAction.action.Enable();
            _sprintAction.action.Disable();
            _crouchAction.action.Disable();
        }

        protected override void Awake()
        {
            base.Awake();
            _moveAction.action.performed += OnMove;
            _jumpAction.action.performed += OnJump;
            _sprintAction.action.started += OnSprint;
            _sprintAction.action.canceled += OnSprint;
            _crouchAction.action.started += OnCrouch;
            _crouchAction.action.canceled += OnCrouch;
        }

        private void OnDestroy()
        {
            _moveAction.action.performed -= OnMove;
            _jumpAction.action.performed -= OnJump;
            _sprintAction.action.started -= OnSprint;
            _sprintAction.action.canceled -= OnSprint;
            _crouchAction.action.started -= OnCrouch;
            _crouchAction.action.canceled -= OnCrouch;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            Move(obj.ReadValue<Vector2>());
        }

        private void OnCrouch(InputAction.CallbackContext obj)
        {
            SetCrouch(obj.started);
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            SetSprint(obj.started);
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            SetJump(true);
        }

        protected override void SetSprint(bool newSprint)
        {
            base.SetSprint(newSprint);
            _fovManager.SetRunning(newSprint);
        }
    }
}
