using UnityEngine;

namespace JackODrain.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement), typeof(PlayerHealth))]
    public sealed class PlayerRespawn : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private PlayerMovement _movement;
        private PlayerHealth _health;
        private Vector2 _lastSafePosition;
        private Vector2 _checkpointPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _movement = GetComponent<PlayerMovement>();
            _health = GetComponent<PlayerHealth>();

            _lastSafePosition = _rigidbody.position;
            _checkpointPosition = _rigidbody.position;
        }

        private void OnEnable()
        {
            _health.Hurt += ReturnToSafeGround;
            _health.Depleted += ReturnToCheckpoint;
        }

        private void OnDisable()
        {
            _health.Hurt -= ReturnToSafeGround;
            _health.Depleted -= ReturnToCheckpoint;
        }

        private void FixedUpdate()
        {
            if (_movement.IsGrounded)
                _lastSafePosition = _rigidbody.position;
        }

        public void SetCheckpoint(Vector2 position)
        {
            _checkpointPosition = position;
            _lastSafePosition = position;
            _health.Refill();
        }

        private void ReturnToSafeGround()
        {
            TeleportTo(_lastSafePosition);
        }

        private void ReturnToCheckpoint()
        {
            TeleportTo(_checkpointPosition);
            _health.Refill();
        }

        private void TeleportTo(Vector2 position)
        {
            _rigidbody.position = position;
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}
