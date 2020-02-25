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
            transform.eulerAngles = _rb.velocity;
        }
    }
}