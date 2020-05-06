using UnityEngine;

namespace World
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private Vector3 _angle;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.angularVelocity = _angle;
        }
    }
}