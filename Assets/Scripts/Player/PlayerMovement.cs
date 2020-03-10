using Cam;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 _speed;
        [SerializeField] private float _sprintModifier = 3;
        [SerializeField] private int _maxJumps = 1;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;
        [SerializeField] private InputActionReference _sprintAction;
        [SerializeField] private InputActionReference _crouchAction;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private FovManager _fovManager;
        private int _jumpCount;
        private bool _isRunning;
        private bool _isJump;
        private bool _isCrouching;
        private Rigidbody _rb;
        private Vector2 _moveDirection;
        private PlayerAnimation _playerAnimation;

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
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerAnimation = GetComponent<PlayerAnimation>();
            _moveAction.action.performed += context => Move(context.ReadValue<Vector2>());
            _jumpAction.action.performed += delegate { SetJump(true); };
            _sprintAction.action.started += delegate { SetSprint(true); };
            _sprintAction.action.canceled += delegate { SetSprint(false); };
            _crouchAction.action.started += delegate { SetCrouch(true); };
            _crouchAction.action.canceled += delegate { SetCrouch(false); };
        }


        private void Move(Vector2 direction)
        {
            _moveDirection = direction;
            _playerAnimation.SetMovement(_moveDirection);
        }

        private void SetJump(bool isJump)
        {
            _isJump = isJump;
        }
        private void SetSprint(bool newSprint)
        {
            _isRunning = newSprint;
            _playerAnimation.SetRunning(_isRunning);
            _fovManager.SetRunning(_isRunning);
        }

        private void SetCrouch(bool newCrouch)
        {
            _isCrouching = newCrouch;
            _playerAnimation.SetCrouching(_isCrouching);
        }

        private void FixedUpdate()
        {
            // Reset jump count when on ground.
            if (_groundCheck.IsGrounded())
            {
                _jumpCount = 0;
            }
            // Move the player.
            var sprintMultiplier = _isRunning ? _sprintModifier : 1;
            var speedMultiplier = Time.fixedDeltaTime * _speed.x * sprintMultiplier;
            var forceX = _moveDirection.x * speedMultiplier * _cameraTransform.right;
            // Get the camera's normalize forward vector.
            var camForward = _cameraTransform.forward;
            camForward.y = 0;
            camForward.Normalize();
            var forceZ =  _moveDirection.y * speedMultiplier * camForward;
            _rb.velocity = forceX + Vector3.up * _rb.velocity.y + forceZ;
            // Jump.
            if (_isJump && _jumpCount < _maxJumps)
            {
                _jumpCount++;
                _groundCheck.Clear();
                _isCrouching = false;
                var velocity = _rb.velocity;
                velocity = Vector3.right * velocity.x + Vector3.forward * velocity.z;
                _rb.velocity = velocity;
                var jumpForce = Vector3.up * _speed.y;
                _rb.AddForce(jumpForce);
            }
            _isJump = false;
        }
    }
}
