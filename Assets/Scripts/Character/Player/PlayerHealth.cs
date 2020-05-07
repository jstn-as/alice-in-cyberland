using UnityEngine;

namespace Character.Player
{
    public class PlayerHealth : CharacterHealth
    {
        [SerializeField] private GameObject _gameOverPanel;
        protected override void Die()
        {
            _gameOverPanel.SetActive(true);
            base.Die();
        }
    }
}