using UnityEngine;
using UnityEngine.InputSystem;

namespace Cam
{
    public class CameraRotate : MonoBehaviour
    {
        [SerializeField] private Vector2 _sensitivity;
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private float _pitchBound;
        private Vector3 _angle;

        public Vector3 GetAngle()
        {
            return _angle;
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

        private void Update()
        {
            transform.eulerAngles = _angle;
        }
    }
}