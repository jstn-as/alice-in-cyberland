using UnityEngine;

namespace World
{
    [CreateAssetMenu(fileName = "Enemy List", menuName = "ScriptableObjects/EnemyList", order = 0)]
    public class EnemyList : ScriptableObject
    {
        [SerializeField] private GameObject[] _enemies;

        public GameObject[] GetEnemies()
        {
            return _enemies;
        }
    }
}