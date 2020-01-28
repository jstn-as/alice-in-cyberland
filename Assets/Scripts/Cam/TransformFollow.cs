using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cam
{
    public class TransformFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector2 _zoomBounds;
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private float _zoomSpeed;
        [SerializeField] private InputActionReference _zoomAction;
        private float _currentZoom;
        private void OnEnable()
        {
            _zoomAction.action.Enable();
        }

        private void OnDisable()
        {
            _zoomAction.action.Disable();
        }

        private void Zoom(float amount)
        {
            _currentZoom += amount * _zoomSpeed;
            _currentZoom = Mathf.Clamp(_currentZoom, _zoomBounds.x, _zoomBounds.y);
        }
        private void Awake()
        {
            _zoomAction.action.performed += context => Zoom(context.ReadValue<float>());
            transform.position = GetTargetPosition();
        }
        private void LateUpdate()
        {
            var lerpPosition = Vector3.Lerp(transform.position, GetTargetPosition(), _speed);
            // Update the current position.
            transform.position = lerpPosition;
        }

        private Vector3 GetTargetPosition()
        {
            // Offset the target position.
            var t = transform;
            var targetX = t.right * _offset.x;
            var targetY = t.up * _offset.y;
            var targetZ = (_offset.z + _currentZoom) * t.forward;
            var offsetPosition = targetX + targetY + targetZ;
            return _target.position + offsetPosition;
        }
    }
}
