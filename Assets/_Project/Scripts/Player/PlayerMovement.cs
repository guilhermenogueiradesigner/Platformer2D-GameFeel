using JackODrain.Core;
using UnityEngine;

namespace JackODrain.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(PlayerInputReader))]
    [RequireComponent(typeof(PlayerAbilities))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        private const float InputDeadZone = 0.01f;
        private const float RestingSpeedTolerance = 0.05f;

        [SerializeField] private MovementSettings _settings;
        [SerializeField] private LayerMask _solidLayers;

        private readonly Countdown _coyoteTime = new();
        private readonly Countdown _jumpBuffer = new();

        private Rigidbody2D _rigidbody;
        private PlayerInputReader _input;
        private PlayerAbilities _abilities;
        private CollisionSensor _sensor;
        private bool _isGrounded;
        private bool _hasAirJump;

        public bool IsGrounded => _isGrounded;
        public int FacingDirection { get; private set; } = 1;
        public Vector2 Velocity => _rigidbody.linearVelocity;
        public CollisionSensor Sensor => _sensor;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _input = GetComponent<PlayerInputReader>();
            _abilities = GetComponent<PlayerAbilities>();

            BoxCollider2D body = GetComponent<BoxCollider2D>();
            _sensor = new CollisionSensor(body, _solidLayers);

            ConfigurePhysicsBody(body);
        }

        private void OnEnable()
        {
            _input.JumpPressed += BufferJump;
        }

        private void OnDisable()
        {
            _input.JumpPressed -= BufferJump;
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            Vector2 velocity = _rigidbody.linearVelocity;

            TickTimers(deltaTime);
            UpdateGroundedState(velocity.y);
            UpdateFacingDirection();

            velocity.x = NextHorizontalSpeed(velocity.x, deltaTime);
            velocity.y = NextVerticalSpeed(velocity.y, deltaTime);
            velocity.y = ResolveCeiling(velocity.y, deltaTime);

            _rigidbody.linearVelocity = velocity;
        }

        private void ConfigurePhysicsBody(BoxCollider2D body)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.gravityScale = 0f;
            _rigidbody.freezeRotation = true;
            _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

            body.sharedMaterial = new PhysicsMaterial2D("Frictionless") { friction = 0f, bounciness = 0f };
        }

        private void TickTimers(float deltaTime)
        {
            _coyoteTime.Tick(deltaTime);
            _jumpBuffer.Tick(deltaTime);
        }

        private void UpdateGroundedState(float verticalSpeed)
        {
            bool wasGrounded = _isGrounded;
            _isGrounded = verticalSpeed <= RestingSpeedTolerance && _sensor.IsTouchingGround();

            if (_isGrounded)
                _hasAirJump = true;

            if (wasGrounded && !_isGrounded)
                _coyoteTime.Start(_settings.CoyoteTime);
        }

        private void UpdateFacingDirection()
        {
            float horizontalInput = _input.Move.x;

            if (Mathf.Abs(horizontalInput) > InputDeadZone)
                FacingDirection = horizontalInput > 0f ? 1 : -1;
        }

        private float NextHorizontalSpeed(float horizontalSpeed, float deltaTime)
        {
            float targetSpeed = _input.Move.x * _settings.MaxRunSpeed;
            float rate = HorizontalRateTowards(targetSpeed);
            return Mathf.MoveTowards(horizontalSpeed, targetSpeed, rate * deltaTime);
        }

        private float HorizontalRateTowards(float targetSpeed)
        {
            bool isSpeedingUp = Mathf.Abs(targetSpeed) > InputDeadZone;

            if (_isGrounded)
                return isSpeedingUp ? _settings.GroundAcceleration : _settings.GroundDeceleration;

            return isSpeedingUp ? _settings.AirAcceleration : _settings.AirDeceleration;
        }

        private float NextVerticalSpeed(float verticalSpeed, float deltaTime)
        {
            if (CanGroundJump())
                return LaunchGroundJump();

            if (CanAirJump())
                return LaunchAirJump();

            return ApplyGravity(verticalSpeed, deltaTime);
        }

        private float ApplyGravity(float verticalSpeed, float deltaTime)
        {
            float gravity = _settings.Gravity * GravityMultiplierFor(verticalSpeed);
            return Mathf.Max(verticalSpeed - gravity * deltaTime, -_settings.MaxFallSpeed);
        }

        private float GravityMultiplierFor(float verticalSpeed)
        {
            if (verticalSpeed < 0f)
                return _settings.FallGravityMultiplier;

            if (verticalSpeed > 0f && !_input.IsJumpHeld)
                return _settings.JumpCutGravityMultiplier;

            return 1f;
        }

        private void BufferJump()
        {
            _jumpBuffer.Start(_settings.JumpBufferTime);
        }

        private bool CanGroundJump()
        {
            return _jumpBuffer.IsRunning && (_isGrounded || _coyoteTime.IsRunning);
        }

        private bool CanAirJump()
        {
            return _jumpBuffer.IsRunning && _hasAirJump && _abilities.Has(Ability.DoubleJump);
        }

        private float LaunchGroundJump()
        {
            ConsumeJumpInput();
            return _settings.JumpLaunchSpeed;
        }

        private float LaunchAirJump()
        {
            ConsumeJumpInput();
            _hasAirJump = false;
            return _settings.DoubleJumpLaunchSpeed;
        }

        private void ConsumeJumpInput()
        {
            _isGrounded = false;
            _coyoteTime.Stop();
            _jumpBuffer.Stop();
        }

        private float ResolveCeiling(float verticalSpeed, float deltaTime)
        {
            if (verticalSpeed <= 0f)
                return verticalSpeed;

            float upwardDistance = verticalSpeed * deltaTime;

            if (!_sensor.IsBlockedAbove(upwardDistance))
                return verticalSpeed;

            if (_sensor.TryFindCornerCorrection(upwardDistance, _settings.CornerCorrectionPixels, out float shift))
            {
                _rigidbody.position += Vector2.right * shift;
                return verticalSpeed;
            }

            return 0f;
        }
    }
}
