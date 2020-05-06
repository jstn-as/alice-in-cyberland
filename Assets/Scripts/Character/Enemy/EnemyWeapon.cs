using UnityEngine;

namespace Character.Enemy
{
    public class EnemyWeapon : CharacterWeapon
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _range;

        protected override void Update()
        {
            var distance = Vector3.Distance(transform.position, _player.position);
            var inRange = distance <= _range;
            IsAttacking = inRange;
            base.Update();
        }
    }
}