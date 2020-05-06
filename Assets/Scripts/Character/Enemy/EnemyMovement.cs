using UnityEngine;

namespace Character.Enemy
{
    public class EnemyMovement : CharacterMovement
    {
        [SerializeField] private Transform _player;
        [SerializeField] private float _walkDistance, _runDistance;

        protected override void FixedUpdate()
        {
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