using Character.Player;
using UnityEngine;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] protected Vector2 _speed;
        [SerializeField] protected float _sprintModifier = 3;
        [SerializeField] protected int _maxJumps = 1;
        [SerializeField] protected GroundCheck _groundCheck;
        [SerializeField] protected Transform _forwardTransform;
        private int _jumpCount;
        private bool _isRunning;
        private bool _isJump;
        private bool _isCrouching;
        private Rigidbody _rb;
        private Vector2 _moveDirection;
        private CharacterAnimation _playerAnimation;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerAnimation = GetComponent<CharacterAnimation>();
        }


        protected void Move(Vector2 direction)
        {
            _moveDirection = direction;
        }

        protected void SetJump(bool isJump)
        {
            _isJump = isJump;
        }
        protected virtual void SetSprint(bool newSprint)
        {
            _isRunning = newSprint;
            _playerAnimation.SetRunning(_isRunning);
        }

        protected void SetCrouch(bool newCrouch)
        {
            _isCrouching = newCrouch;
            _playerAnimation.SetCrouching(_isCrouching);
        }

        protected virtual void FixedUpdate()
        {
            // Reset jump count when on ground.
            if (_groundCheck.IsGrounded())
            {
                _jumpCount = 0;
            }
            // Move the player.
            var sprintMultiplier = _isRunning ? _sprintModifier : 1;
            var speedMultiplier = Time.fixedDeltaTime * _speed.x * sprintMultiplier;
            var forceX = _moveDirection.x * speedMultiplier * _forwardTransform.right;
            // Get the camera's normalize forward vector.
            var camForward = _forwardTransform.forward;
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
