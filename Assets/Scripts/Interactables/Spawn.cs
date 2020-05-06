using UnityEngine;

namespace Interactables
{
    public class Spawn : MonoBehaviour, IInteractable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Renderer _renderer;

        private void Awake()
        {
            _collider.enabled = false;
            _renderer.enabled = false;
        }

        public void Interact()
        {
            _collider.enabled = true;
            _renderer.enabled = true;
        }
    }
}