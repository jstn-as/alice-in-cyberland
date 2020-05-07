using UnityEngine;

namespace Character
{
    public class CharacterRotation : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _speed = .1f;
        protected Vector3 TargetAngle;
        protected Rigidbody Rb;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            Rb.velocity = Vector3.zero;
        }

        protected virtual void Update()
        {
            TargetAngle.y = 0;
            if (TargetAngle.magnitude > 0.1f)
            {
                var targetQuaternion = Quaternion.LookRotation(TargetAngle, Vector3.up);
                var currentQuaternion = Quaternion.LookRotation(transform.forward, Vector3.up);
                var smoothQuaternion = Quaternion.LerpUnclamped(currentQuaternion, targetQuaternion, _speed);
                // transform.forward = targetAngle;
                transform.forward = smoothQuaternion * Vector3.forward;
            }
            else
            {
                Rb.angularVelocity = Vector3.zero;
            }
        }
    }
}