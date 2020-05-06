using UnityEngine;

namespace Interactables.Triggers
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private GameObject[] _interactable;
        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Ignore non projectiles.
            if (!other.transform.CompareTag("Projectile")) return;
            foreach (var go in _interactable)
            {
                go.GetComponent<IInteractable>().Interact();
            }
            _renderer.material = _hitMaterial;
            enabled = false;
        }
    }
}