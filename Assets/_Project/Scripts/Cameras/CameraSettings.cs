using UnityEngine;

namespace JackODrain.Cameras
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Jack O'Drain/Camera Settings")]
    public sealed class CameraSettings : ScriptableObject
    {
        [Header("Horizontal")]
        [Tooltip("Seconds the camera takes to catch up sideways.")]
        [SerializeField, Min(0f)] private float _horizontalSmoothTime = 0.15f;

        [Tooltip("Tiles shown ahead of the facing direction.")]
        [SerializeField, Min(0f)] private float _lookAheadDistance = 2f;

        [Tooltip("Seconds the look-ahead takes to switch sides.")]
        [SerializeField, Min(0f)] private float _lookAheadSmoothTime = 0.5f;

        [Header("Vertical")]
        [Tooltip("Tiles the camera sits above the player's ground height.")]
        [SerializeField] private float _verticalOffset = 1.5f;

        [Tooltip("Seconds the camera takes to settle on a new ground height after landing.")]
        [SerializeField, Min(0f)] private float _landingSmoothTime = 0.3f;

        [Tooltip("Seconds the camera takes to follow a fall.")]
        [SerializeField, Min(0f)] private float _fallingSmoothTime = 0.1f;

        [Tooltip("Tiles the player can rise in the air before the camera follows.")]
        [SerializeField, Min(0f)] private float _riseAllowance = 3.5f;

        [Tooltip("Tiles the player can drop in the air before the camera follows.")]
        [SerializeField, Min(0f)] private float _fallAllowance = 2f;

        public float HorizontalSmoothTime => _horizontalSmoothTime;
        public float LookAheadDistance => _lookAheadDistance;
        public float LookAheadSmoothTime => _lookAheadSmoothTime;
        public float VerticalOffset => _verticalOffset;
        public float LandingSmoothTime => _landingSmoothTime;
        public float FallingSmoothTime => _fallingSmoothTime;
        public float RiseAllowance => _riseAllowance;
        public float FallAllowance => _fallAllowance;
    }
}
