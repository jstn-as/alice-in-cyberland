using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class ExitGame : MonoBehaviour
    {
        [SerializeField] private InputActionReference _exitAction;

        private void OnEnable()
        {
            _exitAction.action.Enable();
        }

        private void OnDisable()
        {
            _exitAction.action.Disable();
        }

        private void Awake()
        {
            _exitAction.action.performed += OnExit;
        }

        private void OnDestroy()
        {
            _exitAction.action.performed -= OnExit;
        }

        private void OnExit(InputAction.CallbackContext obj)
        {
            Exit();
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}