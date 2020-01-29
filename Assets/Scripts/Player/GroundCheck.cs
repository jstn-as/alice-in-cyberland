using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
        private readonly List<Collider> _collidedObjects = new List<Collider>();

        public bool IsGrounded()
        {
            return _collidedObjects.Count > 0;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_collidedObjects.Contains(other)) return;
            _collidedObjects.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_collidedObjects.Contains(other)) return;
            _collidedObjects.Remove(other);
        }

        public void Clear()
        {
            _collidedObjects.Clear();
        }
    }
}
