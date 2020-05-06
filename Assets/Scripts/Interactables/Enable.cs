using UnityEngine;

namespace Interactables
{
    public class Enable : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _gameObject;

        private void Awake()
        {
            _gameObject.SetActive(false);
        }


        public void Interact()
        {
            _gameObject.SetActive(true);
        }
    }
}