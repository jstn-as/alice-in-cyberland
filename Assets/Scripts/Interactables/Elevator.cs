using Character;
using UnityEngine;
using World;

namespace Interactables
{
    public class Elevator : MonoBehaviour, IPlayerInteractable
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _healAmount;
        [SerializeField] private FloorManager _floorManager;
        [SerializeField] private Material _enabledMaterial, _disabledMaterial;
        [SerializeField] private CharacterHealth _player;
        private MeshRenderer _meshRenderer;
        private bool _interactable = true;
        private float _targetHeight;
        private Rigidbody _rb;

        private void Awake()
        {
            _targetHeight = transform.position.y;
            _rb = GetComponent<Rigidbody>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            // Move down if above the target height.
            if (transform.position.y > _targetHeight)
            {
                _rb.isKinematic = false;
                _rb.velocity = Vector3.down * _speed;
            }
            // Snap to the target height otherwise.
            else
            {
                var targetPosition = transform.position;
                targetPosition.y = _targetHeight;
                _rb.isKinematic = true;
                _rb.MovePosition(targetPosition);
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            _interactable = isInteractable;
            _meshRenderer.material = isInteractable ? _enabledMaterial : _disabledMaterial;
        }

        public void Interact(GameObject player)
        {
            if (!_interactable)
                return;
            // Disable this script.
            SetInteractable(false);
            // Create a new floor.
            if (!_floorManager.SpawnFloor())
                return;
            // Heal the player.
            _player.ChangeHealth(_healAmount);
            // Move down.
            _targetHeight -= FloorManager.FloorHeight;
        }
    }
}
