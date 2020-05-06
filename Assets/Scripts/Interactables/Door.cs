using UnityEngine;

namespace Interactables
{
    public class Door : MonoBehaviour, IPlayerInteractable
    {
        private bool _isOpen;
        [SerializeField] private Animator _animator;
        private static readonly int Open = Animator.StringToHash("Open");

        private void ToggleOpen()
        {
            _isOpen = !_isOpen;
            _animator.SetBool(Open, _isOpen);
        }

        public void Interact(GameObject player)
        {
            ToggleOpen();
        }
    }
}