using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class GroundCheck : MonoBehaviour
    {
        private readonly List<Collider> _collidedObjects = new List<Collider>();
        private float _suppressTime;

        public bool IsGrounded()
        {
            if (_suppressTime > 0)
                return false;
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

        public void Suppress(float time)
        {
            _suppressTime = time;
        }

        private void Update()
        {
            _suppressTime -= Time.deltaTime;
        }
    }
}
