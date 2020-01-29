using UnityEngine;
using UnityEngine.InputSystem;

namespace Cam
{
    public class TransformFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector2 _zoomBounds;
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
            // Update the current position.
            transform.position = GetTargetPosition();
        }

        private Vector3 GetTargetPosition()
        {
            // Get the target position.
            var t = transform;
            var targetX = t.right * _offset.x;
            var targetY = t.up * _offset.y;
            var targetZ = (_offset.z + _currentZoom) * t.forward;
            var offsetPosition = targetX + targetY + targetZ;
            var position = _target.position;
            var targetPosition = position + offsetPosition;
            
            var ray = new Ray(position, offsetPosition);
            if (Physics.Raycast(ray, out var hit, offsetPosition.magnitude, 9))
            {
                targetPosition = hit.point - offsetPosition.normalized * 0.1f;
            }
            
            return targetPosition;
        }
    }
}
