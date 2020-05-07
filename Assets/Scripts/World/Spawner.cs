using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace World
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _entities;
        [SerializeField] private Transform[] _spawnPoints;
        private Elevator _elevator;
        private List<GameObject> _spawnedEntities = new List<GameObject>();

        private void Start()
        {
            _elevator = FindObjectOfType<Elevator>();
            for (var i = 0; i < _entities.Length; i++)
            {
                var spawnPosition = _spawnPoints[i].position;
                var prefab = _entities[i];
                var newEntity = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
                _spawnedEntities.Add(newEntity);
            }
        }

        public void SetEntities(GameObject[] newEntities)
        {
            _entities = newEntities;
        }

        public void RemoveEntity(GameObject entity)
        {
            _spawnedEntities.Remove(entity);
            // Enable the elevator once all enemies are killed.
            if (_spawnedEntities.Count == 0)
            {
                _elevator.SetInteractable(true);
            }
        }
    }
}
