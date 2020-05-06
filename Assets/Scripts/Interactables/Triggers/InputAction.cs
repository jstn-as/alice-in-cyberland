using UnityEngine;
using UnityEngine.InputSystem;

namespace Interactables.Triggers
{
    public class InputAction : MonoBehaviour
    {
        [SerializeField] private InputActionReference _inputAction;
        [SerializeField] private GameObject[] _interactables;

        private void Awake()
        {
            _inputAction.action.performed += ActionOnPerformed;
        }

        private void OnEnable()
        {
            _inputAction.action.Enable();
        }

        private void OnDisable()
        {
            _inputAction.action.Disable();
        }
        
        private void OnDestroy()
        {
            _inputAction.action.performed -= ActionOnPerformed;
        }

        private void ActionOnPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            // Interact with each object.
            foreach (var interactable in _interactables)
            {
                // Get all the interactables on the target object.
                var interactables = interactable.GetComponents<IInteractable>();
                foreach (var iComponent in interactables)
                {
                    // Interact with all the interactables.
                    iComponent.Interact();
                }
            }
            // Unregister the action.
            _inputAction.action.performed -= ActionOnPerformed;
        }
    }
}