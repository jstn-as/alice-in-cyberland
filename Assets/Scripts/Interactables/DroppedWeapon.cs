using Character;
using Character.Player;
using UnityEngine;
using Weapon;

namespace Interactables
{
    public class DroppedWeapon : MonoBehaviour, IPlayerInteractable
    {
        [SerializeField] private WeaponObject _weapon;
        public void Interact(GameObject player)
        {
            var hasAdded = player.GetComponent<CharacterInventory>().AddWeapon(_weapon);
            if (hasAdded) Destroy(gameObject);
        }
    }
}