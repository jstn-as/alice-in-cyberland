using UnityEngine;

namespace Interactables
{
    public class AnimationTrigger : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _triggerName;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact()
        {
            _animator.SetTrigger(_triggerName);
        }
    }
}