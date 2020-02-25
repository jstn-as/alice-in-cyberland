using UnityEngine;

namespace Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private GroundCheck _groundCheck;
        private CapsuleCollider _collider;
        private Rigidbody _rb;
        private Vector2 _movement;
        private Animator _animator; 
        private bool _isRunning;
        private bool _isCrouching;
        private int _currentTrigger = int.MinValue;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int CIdle = Animator.StringToHash("C Idle");
        private static readonly int CWalk = Animator.StringToHash("C Walk");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int MoveX = Animator.StringToHash("Move X");
        private static readonly int MoveY = Animator.StringToHash("Move Y");

        public void SetMovement(Vector2 newMovement)
        {
            _movement = newMovement;
            _animator.SetFloat(MoveX, _movement.x);
            _animator.SetFloat(MoveY, _movement.y);
        }
        public void SetRunning(bool isRunning)
        {
            _isRunning = isRunning;
        }
        public void SetCrouching(bool isCrouching)
        {
            _isCrouching = isCrouching;
        }
        public void UpdateCollider(Vector2 size, Vector3 center)
        {
            _collider.center = center;
            _collider.height = size.y;
            _collider.radius = size.x;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            _animator.SetTrigger(Fall);
            _animator.SetTrigger(Idle);
        }

        private void Update()
        {
            // Standing.
            if (_groundCheck.IsGrounded())
            {
                // Moving.
                if (Mathf.Abs(_movement.x) + Mathf.Abs(_movement.y) > 0)
                {
                    if (_isRunning)
                    {
                        SetTrigger(Run);
                    }
                    else
                    {
                        SetTrigger(_isCrouching ? CWalk : Walk);
                    }
                }
                // Idle.
                else
                {
                    SetTrigger(_isCrouching ? CIdle : Idle);
                }
            }
            else
            {
                // Jumping
                if (_rb.velocity.y > 0)
                {
                    SetTrigger(Jump);
                }
                // Falling.
                else
                {
                    SetTrigger(Fall);
                }
            }
        }
        private void SetTrigger(int newTrigger)
        {
            if (newTrigger == _currentTrigger) return;
            _currentTrigger = newTrigger;
            _animator.SetTrigger(newTrigger);
        }
    }
}