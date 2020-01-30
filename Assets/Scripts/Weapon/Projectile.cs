using System;
using UnityEngine;

namespace Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxLifetime;
        private float _lifetime;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _lifetime += Time.deltaTime;
            if (_lifetime > _maxLifetime)
            {
                Destroy(gameObject);
            }
            _rb.velocity = transform.forward * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                var health = other.GetComponent<Health>();
                health.ChangeHealth(-_damage);
            }
            Destroy(gameObject);
        }
    }
}