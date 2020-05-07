using UnityEngine;

namespace Character.Enemy
{
    public class EnemyMovement : CharacterMovement
    {
        private Transform _player;
        private PlayerFinder _playerFinder;
        [SerializeField] private float _walkDistance, _runDistance;

        protected override void Awake()
        {
            _playerFinder = GetComponent<PlayerFinder>();
            base.Awake();
        }

        private void Start()
        {
            _player = _playerFinder.GetPlayer();
        }

        private void Update()
        {
            // Skip if the player is missing.
            if (!_player)
                return;
            // Move towards the player.
            var distance = Vector3.Distance(transform.position, _player.position);
            if (distance >= _runDistance)
            {
                SetSprint(true);
                Move(Vector2.up);
            }
            else if (distance >= _walkDistance)
            {
                SetSprint(false);
                Move(Vector2.up);
            }
            else
            {
                SetSprint(false);
                Move(Vector2.zero);
            }
            
            base.FixedUpdate();
        }
    }
}