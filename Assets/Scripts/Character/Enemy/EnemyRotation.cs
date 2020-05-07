using UnityEngine;

namespace Character.Enemy
{
    public class EnemyRotation : CharacterRotation
    {
        private Transform _player;
        private PlayerFinder _playerFinder;
        protected override void Awake()
        {
            _playerFinder = GetComponent<PlayerFinder>();
            base.Awake();
        }

        private void Start()
        {
            _player = _playerFinder.GetPlayer();
        }

        protected override void Update()
        {
            if (!_player)
                return;
            var playerPosition = _player.position;
            playerPosition.y = 0;
            var position = transform.position;
            position.y = 0;
            TargetAngle = playerPosition - position;
            base.Update();
        }
    }
}