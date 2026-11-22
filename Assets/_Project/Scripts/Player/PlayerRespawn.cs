using UnityEngine;

namespace JackODrain.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement), typeof(PlayerHealth))]
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class PlayerRespawn : MonoBehaviour
    {
        [Tooltip("Layers of everything that causes damage.")]
        [SerializeField] private LayerMask _hazardLayers;

        [Tooltip("Tiles kept between the player and the platform edges. Narrower platforms use their center.")]
        [SerializeField, Min(0f)] private float _edgeClearance = 0.5f;

        [Tooltip("Tiles of distance from any hazard for a position to count as safe.")]
        [SerializeField, Min(0f)] private float _hazardClearance = 0.5f;

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
            if (TryFindSafePosition(out Vector2 safePosition))
                _lastSafePosition = safePosition;
        }

        public void SetCheckpoint(Vector2 position)
        {
            _checkpointPosition = position;
            _lastSafePosition = position;
            _health.Refill();
        }

        private bool TryFindSafePosition(out Vector2 safePosition)
        {
            safePosition = _rigidbody.position;
            CollisionSensor sensor = _movement.Sensor;

            if (_health.IsInvulnerable || !_movement.IsGrounded || !sensor.HasGroundUnderBothFeet())
                return false;

            float bodyCenterX = _body.bounds.center.x;
            float measuringReach = 2f * (_edgeClearance + sensor.HalfWidth);
            float safeCenterX = SafeGround.StandingPointAwayFromEdges(
                bodyCenterX,
                sensor.HalfWidth,
                sensor.FreeGroundBeyondFoot(-1, measuringReach),
                sensor.FreeGroundBeyondFoot(1, measuringReach),
                _edgeClearance);

            float shift = safeCenterX - bodyCenterX;
            safePosition += Vector2.right * shift;

            return !IsNearHazard(shift);
        }

        private bool IsNearHazard(float horizontalShift)
        {
            Bounds bounds = _body.bounds;
            Vector2 center = (Vector2)bounds.center + Vector2.right * horizontalShift;
            Vector2 searchSize = (Vector2)bounds.size + Vector2.one * (_hazardClearance * 2f);
            return Physics2D.OverlapBox(center, searchSize, 0f, _hazardLayers) != null;
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
