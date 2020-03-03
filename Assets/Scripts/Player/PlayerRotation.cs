using UnityEngine;

namespace Player
{
    public class PlayerRotation : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_rb.velocity.magnitude < 0.1f) return; 
            var targetAngle = _rb.velocity;
            targetAngle.y = 0;
            var targetRotation = Quaternion.LookRotation(targetAngle, Vector3.up);
            var smoothRotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            transform.forward = targetAngle;
        }
    }
}