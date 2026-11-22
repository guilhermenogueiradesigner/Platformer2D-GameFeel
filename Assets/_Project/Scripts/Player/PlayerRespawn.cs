using UnityEngine;

namespace JackODrain.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement), typeof(PlayerHealth))]
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class PlayerRespawn : MonoBehaviour
    {
        [Tooltip("Layers of everything that causes damage.")]
        [SerializeField] private LayerMask _hazardLayers;

        [Tooltip("Tiles of solid ground required beyond each foot for a position to count as safe.")]
        [SerializeField, Min(0f)] private float _edgeClearance = 0.5f;

        [Tooltip("Tiles of distance from any hazard for a position to count as safe.")]
        [SerializeField, Min(0f)] private float _hazardClearance = 1f;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _body;
        private PlayerMovement _movement;
        private PlayerHealth _health;
        private Vector2 _lastSafePosition;
        private Vector2 _checkpointPosition;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _body = GetComponent<BoxCollider2D>();
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
            if (IsSafeToRemember())
                _lastSafePosition = _rigidbody.position;
        }

        public void SetCheckpoint(Vector2 position)
        {
            _checkpointPosition = position;
            _lastSafePosition = position;
            _health.Refill();
        }

        private bool IsSafeToRemember()
        {
            return !_health.IsInvulnerable
                && _movement.IsStandingAwayFromEdges(_edgeClearance)
                && !IsNearHazard();
        }

        private bool IsNearHazard()
        {
            Bounds bounds = _body.bounds;
            Vector2 searchSize = (Vector2)bounds.size + Vector2.one * (_hazardClearance * 2f);
            return Physics2D.OverlapBox(bounds.center, searchSize, 0f, _hazardLayers) != null;
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
