using UnityEngine;

namespace Character.Enemy
{
    public class EnemyAnimation : CharacterAnimation
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _aimDistance;

        private void Update()
        {
            var distance = Vector3.Distance(transform.position, _player.position);
            SetAim(distance <= _aimDistance);
        }
    }
}