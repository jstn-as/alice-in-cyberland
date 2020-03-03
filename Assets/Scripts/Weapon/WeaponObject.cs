using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Weapon", order = 0)]
    public class WeaponObject : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private float _fireRate;
        [SerializeField] private int _magSize;
        [SerializeField] private Vector2 _recoil;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Sprite _sprite;

        public GameObject GetPrefab()
        {
            return _prefab;
        }

        public GameObject GetProjectile()
        {
            return _projectile;
        }

        public float GetFireRate()
        {
            return _fireRate;
        }

        public int GetMagazineSize()
        {
            return _magSize;
        }
        public Sprite GetSprite()
        {
            return _sprite;
        }

        public Vector2 GetRecoil()
        {
            return _recoil;
        }
    }
}