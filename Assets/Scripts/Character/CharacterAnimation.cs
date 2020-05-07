using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private GroundCheck _groundCheck;
        private readonly Vector3 _offset = Vector3.up * 40;
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private Transform _aimForward;
        [SerializeField] private GameObject _fist;
        private Vector2 _movementInput;
        protected bool IsCrouching;
        protected bool IsAiming;
        private CharacterWeapon _characterWeapon;
        private Quaternion _currentRotation;
        private Transform _chest;
        private Rigidbody _rb;
        private Animator _animator;
        private bool _isRunning;
        private bool _isReloading;
        private bool _isPunching;
        private float _currentWeight;
        private int _animLayerIndex;
        private float _timeSincePunch;
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int CIdle = Animator.StringToHash("C Idle");
        private static readonly int CWalk = Animator.StringToHash("C Walk");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int ReloadHash = Animator.StringToHash("Reload");
        private static readonly int PunchHash = Animator.StringToHash("Punch");
        private static readonly int Fist = Animator.StringToHash("Fist");
        private static readonly int Pistol = Animator.StringToHash("Pistol");
        private int _currentTrigger = Idle;

        public void SetRunning(bool isRunning)
        {
            _isRunning = isRunning;
        }
        public void SetCrouching(bool isCrouching)
        {
            IsCrouching = isCrouching;
        }
        public bool IsPunching()
        {
            return _isPunching;
        }
        public bool IsReloading()
        {
            return _isReloading;
        }
        public void Reload()
        {
            // Return if already reloading.
            if (_isReloading)
                return;
            _isReloading = true;
            _animator.SetTrigger(ReloadHash);
        }
        public void StopReloading()
        {
            _isReloading = false;
            
        }
        public void Punch()
        {
            _isPunching = true;
            _animator.SetTrigger(PunchHash);
            _timeSincePunch = 2;
            _fist.SetActive(true);
        }

        private void PunchFinish()
        {
            _fist.SetActive(false);
            _isPunching = false;
            _timeSincePunch = 0;
        }

        public void SetMovementInput(Vector2 movementInput)
        {
            _movementInput = movementInput;
        }

        public void SetAim(bool isAiming)
        {
            // Only update if changed.
            if (IsAiming == isAiming)
                return;
            // Ignore if reloading.
            if (_isReloading)
                return;
            IsAiming = isAiming;
            // Set the idle to fists if no weapon is equipped.
            var weaponEquipped = _characterWeapon.GetCurrentWeapon();
            _animator.SetTrigger(weaponEquipped ? Pistol : Fist);
            
            _characterWeapon.enabled = IsAiming;
        }
        protected virtual void Awake()
        {
            _characterWeapon = GetComponent<CharacterWeapon>();
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _animLayerIndex = _animator.GetLayerIndex("Upper Body");
            _chest = _animator.GetBoneTransform(HumanBodyBones.Chest);
            _animator.SetTrigger(Idle);
        }
        protected virtual void FixedUpdate()
        {
            if (_timeSincePunch < 0)
                PunchFinish();
            else
                _timeSincePunch -= Time.deltaTime;
            // Standing.
            if (_groundCheck.IsGrounded())
            {
                // Moving.
                if (_movementInput.magnitude > 0.1f)
                {
                    if (_isRunning)
                        SetTrigger(Run);
                    else
                        SetTrigger(IsCrouching ? CWalk : Walk);
                }
                // Idle.
                else
                    SetTrigger(IsCrouching ? CIdle : Idle);
            }
            // Jumping
            else
                SetTrigger(_rb.velocity.y > 0 ? Jump : Fall);
        }
        protected virtual void LateUpdate()
        {
            // Lerp between layer weights.
            var targetWeight = IsAiming ? 1 : 0;
            if (_isReloading)
                targetWeight = 1;
            _currentWeight = Mathf.Lerp(_currentWeight, targetWeight, _speed);
            _animator.SetLayerWeight(_animLayerIndex, _currentWeight);
            // Interpolate between the chest animations.
            var targetRotation = IsAiming ? _aimForward.rotation * Quaternion.Euler(_offset) : _chest.rotation;
            _currentRotation = Quaternion.Lerp(_currentRotation, targetRotation, _speed);
            _chest.rotation = _currentRotation;
        }
        private void SetTrigger(int newTrigger)
        {
            if (newTrigger == _currentTrigger)
                return;
            _animator.ResetTrigger(_currentTrigger);
            _currentTrigger = newTrigger;
            _animator.SetTrigger(newTrigger);
        }
    }
}