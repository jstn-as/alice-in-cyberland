using UnityEngine;

namespace Character.Enemy
{
    public class EnemyWeapon : CharacterWeapon
    {
        [SerializeField] private float _range;
        [SerializeField] private float _shootRate;
        private float _timeSinceShoot;
        private Transform _player;
        private readonly Vector3 _offset = Vector3.up * 0.9f;
        private PlayerFinder _playerFinder;

        protected override void Awake()
        {
            _playerFinder = GetComponent<PlayerFinder>();
            base.Awake();
        }

        protected override void Start()
        {
            _player = _playerFinder.GetPlayer();
            base.Start();
        }

        protected override void Update()
        {
            IsAttacking = false;
            if (!_player)
                return;
            // Reload if out of ammo.
            if (CurrentWeapon && Inventory.GetAmmo() <= 0)
                CharacterAnimation.Reload();
            // Shoot otherwise.
            else
            {
                TargetPoint = _player.position + _offset;
                var distance = Vector3.Distance(transform.position, TargetPoint);
                var inRange = distance <= _range;
                IsAttacking = inRange;
            }
            base.Update();
        }

        protected override void Shoot()
        {
            if (_timeSinceShoot < _shootRate)
            {
                _timeSinceShoot += Time.deltaTime;
                return;
            }
            _timeSinceShoot = 0;
            base.Shoot();
        }
    }
}