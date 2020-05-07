using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private GameObject _floorPrefab;
        [SerializeField] private List<GameObject> _floors;
        [SerializeField] private EnemyList[] _enemies;
        [SerializeField] private GameObject _gameOverPanel;
        public const float FloorHeight = 10;

        public bool SpawnFloor()
        {
            // Check if out of levels.
            if (_floors.Count == _enemies.Length + 1)
            {
                _gameOverPanel.SetActive(true);
                return false;
            }
            var spawnPosition = _floors.Count * FloorHeight * Vector3.down;
            var newFloor = Instantiate(_floorPrefab, spawnPosition, Quaternion.identity, transform);
            var spawner = newFloor.GetComponent<Spawner>();
            var floorName = newFloor.GetComponent<FloorName>();
            floorName.SetName($"Floor {_floors.Count}");
            spawner.SetEntities(_enemies[_floors.Count - 1].GetEnemies());
            _floors.Add(newFloor);
            return true;
        }
    }
}
