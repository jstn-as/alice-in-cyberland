using Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private InputActionReference _interactAction;
        [SerializeField] private GameObject _interactUi;
        [SerializeField] private Transform _camera;
        private IPlayerInteractable _interactable;

        private void Awake()
        {
            _interactAction.action.performed += context => { Interact(); };
        }

        private void OnEnable()
        {
            _interactAction.action.Enable();
        }

        private void OnDisable()
        {
            _interactAction.action.Disable();
        }

        private void FixedUpdate()
        {
            var ray = new Ray(_camera.position, _camera.forward);
            if (Physics.Raycast(ray, out var hitInfo, _range))
            {
                _interactable = hitInfo.collider.GetComponent<IPlayerInteractable>();
            }
            else
            {
                _interactable = null;
            }
            _interactUi.SetActive(_interactable != null);
        }

        private void Interact()
        {
            _interactable?.Interact(gameObject);
        }
    }
}