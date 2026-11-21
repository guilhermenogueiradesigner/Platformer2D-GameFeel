using UnityEngine;

namespace JackODrain.Player
{
    [CreateAssetMenu(fileName = "MovementSettings", menuName = "Jack O'Drain/Movement Settings")]
    public sealed class MovementSettings : ScriptableObject
    {
        [Header("Run")]
        [Tooltip("Tiles per second.")]
        [SerializeField, Min(0f)] private float _maxRunSpeed = 7f;

        [Tooltip("Tiles per second squared.")]
        [SerializeField, Min(0f)] private float _groundAcceleration = 80f;

        [Tooltip("Tiles per second squared.")]
        [SerializeField, Min(0f)] private float _groundDeceleration = 90f;

        [Tooltip("Tiles per second squared.")]
        [SerializeField, Min(0f)] private float _airAcceleration = 60f;

        [Tooltip("Tiles per second squared.")]
        [SerializeField, Min(0f)] private float _airDeceleration = 40f;

        [Header("Jump")]
        [Tooltip("Tiles reached with the button held.")]
        [SerializeField, Min(0.1f)] private float _jumpHeight = 3f;

        [Tooltip("Seconds from takeoff to the highest point.")]
        [SerializeField, Min(0.05f)] private float _timeToJumpApex = 0.35f;

        [Tooltip("Gravity multiplier while falling.")]
        [SerializeField, Min(1f)] private float _fallGravityMultiplier = 1.6f;

        [Tooltip("Gravity multiplier while rising after the button is released.")]
        [SerializeField, Min(1f)] private float _jumpCutGravityMultiplier = 2.5f;

        [Tooltip("Tiles per second.")]
        [SerializeField, Min(0f)] private float _maxFallSpeed = 18f;

        [Header("Forgiveness")]
        [Tooltip("Seconds a jump is still allowed after leaving a ledge.")]
        [SerializeField, Min(0f)] private float _coyoteTime = 0.1f;

        [Tooltip("Seconds a jump press is remembered before landing.")]
        [SerializeField, Min(0f)] private float _jumpBufferTime = 0.1f;

        [Tooltip("Pixels the body can be nudged sideways to slip past a ceiling corner.")]
        [SerializeField, Min(0)] private int _cornerCorrectionPixels = 4;

        public float MaxRunSpeed => _maxRunSpeed;
        public float GroundAcceleration => _groundAcceleration;
        public float GroundDeceleration => _groundDeceleration;
        public float AirAcceleration => _airAcceleration;
        public float AirDeceleration => _airDeceleration;
        public float FallGravityMultiplier => _fallGravityMultiplier;
        public float JumpCutGravityMultiplier => _jumpCutGravityMultiplier;
        public float MaxFallSpeed => _maxFallSpeed;
        public float CoyoteTime => _coyoteTime;
        public float JumpBufferTime => _jumpBufferTime;
        public int CornerCorrectionPixels => _cornerCorrectionPixels;

        public float Gravity => JumpPhysics.GravityFor(_jumpHeight, _timeToJumpApex);
        public float JumpLaunchSpeed => JumpPhysics.LaunchSpeedFor(_jumpHeight, _timeToJumpApex);
    }
}
