using UnityEngine;

namespace JackODrain.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public sealed class PlayerAnimator : MonoBehaviour
    {
        private const float RunSpeedThreshold = 0.1f;

        private static readonly string[] RequiredStateNames = { "Idle", "Run", "Jump", "Fall" };

        private static readonly int IdleState = Animator.StringToHash("Idle");
        private static readonly int RunState = Animator.StringToHash("Run");
        private static readonly int JumpState = Animator.StringToHash("Jump");
        private static readonly int FallState = Animator.StringToHash("Fall");

        private PlayerMovement _movement;
        private Animator _animator;
        private SpriteRenderer _sprite;
        private int _currentState;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _animator = GetComponentInChildren<Animator>();
            _sprite = GetComponentInChildren<SpriteRenderer>();

            WarnAboutMissingStates();
        }

        private void LateUpdate()
        {
            _sprite.flipX = _movement.FacingDirection < 0;
            PlayIfChanged(StateForMovement());
        }

        private int StateForMovement()
        {
            Vector2 velocity = _movement.Velocity;

            if (_movement.IsGrounded)
                return Mathf.Abs(velocity.x) > RunSpeedThreshold ? RunState : IdleState;

            return velocity.y > 0f ? JumpState : FallState;
        }

        private void PlayIfChanged(int state)
        {
            if (state == _currentState)
                return;

            _currentState = state;
            _animator.Play(state);
        }

        private void WarnAboutMissingStates()
        {
            foreach (string stateName in RequiredStateNames)
            {
                if (!_animator.HasState(0, Animator.StringToHash(stateName)))
                    Debug.LogWarning($"{name}: the Animator has no '{stateName}' state. State names must match the Aseprite tags.", this);
            }
        }
    }
}
