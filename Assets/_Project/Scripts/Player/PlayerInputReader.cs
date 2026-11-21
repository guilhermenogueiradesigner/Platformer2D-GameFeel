using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JackODrain.Player
{
    public sealed class PlayerInputReader : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveAction;
        [SerializeField] private InputActionReference _jumpAction;

        public event Action JumpPressed;

        public Vector2 Move => _moveAction.action.ReadValue<Vector2>();
        public bool IsJumpHeld => _jumpAction.action.IsPressed();

        private void OnEnable()
        {
            _moveAction.action.Enable();
            _jumpAction.action.Enable();
            _jumpAction.action.performed += HandleJumpPerformed;
        }

        private void OnDisable()
        {
            _jumpAction.action.performed -= HandleJumpPerformed;
        }

        private void HandleJumpPerformed(InputAction.CallbackContext context)
        {
            JumpPressed?.Invoke();
        }
    }
}
