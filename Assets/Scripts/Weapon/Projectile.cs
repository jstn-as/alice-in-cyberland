using Character;
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
                Destroy(gameObject);
            _rb.velocity = transform.forward * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            var health = other.GetComponent<CharacterHealth>();
            // Only damage the object if it has a health component.
            if (health)
                health.Damage(_damage);
            Destroy(gameObject);
        }
    }
}