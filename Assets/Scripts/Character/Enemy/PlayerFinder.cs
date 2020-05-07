using Character.Player;
using UnityEngine;

namespace Character.Enemy
{
    public class PlayerFinder : MonoBehaviour
    {
        private Transform _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerMovement>().transform;
        }

        public Transform GetPlayer()
        {
            return _player;
        }
    }
}