using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Weapon", order = 0)]
    public class WeaponObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private int _damage;
        [SerializeField] private float _fireRate;
        [SerializeField] private int _magSize;

        public GameObject GetPrefab()
        {
            return _prefab;
        }

        public GameObject GetProjectile()
        {
            return _projectile;
        }
    }
}