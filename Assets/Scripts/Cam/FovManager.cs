using System;
using UnityEngine;

namespace Cam
{
    public class FovManager : MonoBehaviour
    {
        [SerializeField] private float _normalFov;
        [SerializeField] private float _runningFov;
        [SerializeField] private float _aimingFov;
        [SerializeField, Range(0, 1)] private float _speed;
        private bool _isRunning;
        private bool _isAiming;
        private float _targetFov;
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            UpdateTarget();
        }

        private void Update()
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFov, _speed);
        }

        public void SetRunning(bool isRunning)
        {
            _isRunning = isRunning;
            UpdateTarget();
        }

        public void SetAiming(bool isAiming)
        {
            _isAiming = isAiming;
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            if (_isAiming)
            {
                _targetFov = _aimingFov;
            }
            else if (_isRunning)
            {
                _targetFov = _runningFov;
            }
            else
            {
                _targetFov = _normalFov;
            }
        }
    }
}