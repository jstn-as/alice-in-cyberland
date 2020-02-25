using UnityEngine;
using UnityEngine.InputSystem;

namespace Cam
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private Vector2 _sensitivity;
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private float _pitchBound;
        [SerializeField, Range(0, 1)] private float _recoilSpeed;
        private Vector2 _recoil;
        private Vector3 _angle;

        // Return the current target facing angle of the camera.
        public Vector3 GetAngle()
        {
            // Apply recoil to the angle.
            var targetAngle = _angle;
            targetAngle.x -= _recoil.y;
            targetAngle.y += _recoil.x;
            return targetAngle;
        }
        private void Awake()
        {
            _angle = transform.eulerAngles;
            _lookAction.action.performed += context => Look(context.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _lookAction.action.Enable();
        }

        private void OnDisable()
        {
            _lookAction.action.Disable();
        }

        private void Look(Vector2 direction)
        {
            _angle.x -= direction.y * _sensitivity.y;
            _angle.x = Mathf.Clamp(_angle.x, -_pitchBound, _pitchBound);
            _angle.y += direction.x * _sensitivity.x;
        }

        public void AddRecoil(Vector2 recoil)
        {
            _recoil += recoil;
        }

        private void Update()
        {
            // Slowly remove recoil.
            _recoil = Vector2.Lerp(_recoil, Vector2.zero, _recoilSpeed);
            // Apply recoil to the angle.
            var targetAngle = _angle;
            targetAngle.x -= _recoil.y;
            targetAngle.y += _recoil.x;
            transform.eulerAngles = targetAngle;
        }
    }
}