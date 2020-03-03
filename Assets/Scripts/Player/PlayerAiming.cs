using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerAiming : MonoBehaviour
    {
        [SerializeField] private InputActionReference _aimAction;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Camera _camera;
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private PlayerWeapon _playerWeapon;
        [SerializeField] private float _fovMultiplier;
        private float _normalFov;
        private Quaternion _currentRotation;
        private bool _isAiming;
        private Animator _animator;
        private Transform _chest;
        private int _animLayerIndex;

        private void OnEnable()
        {
            _aimAction.action.Enable();
        }

        private void OnDisable()
        {
            _aimAction.action.Disable();
        }

        private void Awake()
        {
            _aimAction.action.performed += delegate { SetAim(true); };
            _aimAction.action.canceled += delegate { SetAim(false); };
            _animator = GetComponent < Animator>();
            _animLayerIndex = _animator.GetLayerIndex("Upper Body");
            _chest = _animator.GetBoneTransform(HumanBodyBones.Chest);
            _normalFov = _camera.fieldOfView;
        }

        private void LateUpdate()
        {
            var targetFov = _isAiming ? _normalFov * _fovMultiplier : _normalFov;
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFov, 0.1f);
            if (!_isAiming)
            {
                _animator.SetLayerWeight(_animLayerIndex, 0);
                _currentRotation = _chest.rotation;
                return;
            }
            _animator.SetLayerWeight(_animLayerIndex, 1);
            var targetRotation = _camera.transform.rotation * Quaternion.Euler(_offset);
            _currentRotation = Quaternion.Lerp(_currentRotation, targetRotation, _speed);
            _chest.rotation = _currentRotation;
        }

        private void SetAim(bool newAim)
        {
            _isAiming = newAim;
            _playerWeapon.enabled = _isAiming;
        }
    }
}