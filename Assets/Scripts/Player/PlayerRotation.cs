using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        private Vector3 _targetAngle;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var targetAngle = _rb.velocity;
            targetAngle.y = 0;
            if (targetAngle.magnitude > 0.1f)
            {
                _targetAngle = targetAngle;
            }
            transform.forward = _targetAngle;
        }
    }
}