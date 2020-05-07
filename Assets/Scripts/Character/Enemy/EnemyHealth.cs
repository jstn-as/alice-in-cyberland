using UnityEngine;
using World;

namespace Character.Enemy
{
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField] private GameObject _drop;
        protected override void Die()
        {
            var spawner = GetComponentInParent<Spawner>();
            spawner.RemoveEntity(gameObject);
            // Drop the item.
            if (_drop)
            {
                Instantiate(_drop, transform.position, Quaternion.identity, null);
            }
            base.Die();
        }
    }
}