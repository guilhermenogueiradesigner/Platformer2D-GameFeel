using JackODrain.Player;
using UnityEngine;

namespace JackODrain.Cameras
{
    public sealed class PlatformerCamera : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _target;
        [SerializeField] private CameraSettings _settings;

        private float _groundHeight;
        private float _lookAhead;
        private float _lookAheadVelocity;
        private float _horizontalVelocity;
        private float _verticalVelocity;

        private Vector2 TargetPosition => _target.transform.position;

        private void Start()
        {
            _groundHeight = TargetPosition.y;
            _lookAhead = _target.FacingDirection * _settings.LookAheadDistance;
            MoveTo(TargetPosition.x + _lookAhead, _groundHeight + _settings.VerticalOffset);
        }

        private void LateUpdate()
        {
            UpdateGroundHeight();
            UpdateLookAhead();

            float x = Mathf.SmoothDamp(transform.position.x, TargetPosition.x + _lookAhead, ref _horizontalVelocity, _settings.HorizontalSmoothTime);
            float y = Mathf.SmoothDamp(transform.position.y, _groundHeight + _settings.VerticalOffset, ref _verticalVelocity, VerticalSmoothTime());

            MoveTo(x, y);
        }

        private void UpdateGroundHeight()
        {
            float playerHeight = TargetPosition.y;

            if (_target.IsGrounded)
                _groundHeight = playerHeight;
            else if (playerHeight > _groundHeight + _settings.RiseAllowance)
                _groundHeight = playerHeight - _settings.RiseAllowance;
            else if (playerHeight < _groundHeight - _settings.FallAllowance)
                _groundHeight = playerHeight + _settings.FallAllowance;
        }

        private void UpdateLookAhead()
        {
            float desiredLookAhead = _target.FacingDirection * _settings.LookAheadDistance;
            _lookAhead = Mathf.SmoothDamp(_lookAhead, desiredLookAhead, ref _lookAheadVelocity, _settings.LookAheadSmoothTime);
        }

        private float VerticalSmoothTime()
        {
            bool isFalling = !_target.IsGrounded && _target.Velocity.y < 0f;
            return isFalling ? _settings.FallingSmoothTime : _settings.LandingSmoothTime;
        }

        private void MoveTo(float x, float y)
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}
