using UnityEngine;

namespace Character.Enemy
{
    public class EnemyAnimation : CharacterAnimation
    {
        private Transform _player;
        private PlayerFinder _playerFinder;
        [SerializeField] private float _aimDistance;

        protected override void Awake()
        {
            _playerFinder = GetComponent<PlayerFinder>();
            base.Awake();
        }

        private void Start()
        {
            _player = _playerFinder.GetPlayer();
        }

        protected override void FixedUpdate()
        {
            // Skip if the player is missing.
            if (!_player)
            {
                SetAim(false);
                return;
            }
            var distance = Vector3.Distance(transform.position, _player.position);
            SetAim(distance <= _aimDistance);
            base.FixedUpdate();
        }
    }
}