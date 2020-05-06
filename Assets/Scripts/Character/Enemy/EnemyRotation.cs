using UnityEngine;

namespace Character.Enemy
{
    public class EnemyRotation : CharacterRotation
    {
        [SerializeField] private Transform _player;
        protected override void Update()
        {
            var playerPosition = _player.position;
            playerPosition.y = 0;
            var position = transform.position;
            position.y = 0;
            TargetAngle = playerPosition - position;
            base.Update();
        }
    }
}