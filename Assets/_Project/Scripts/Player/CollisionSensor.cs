using JackODrain.Core;
using UnityEngine;

namespace JackODrain.Player
{
    public sealed class CollisionSensor
    {
        private const float ProbeDistance = 0.05f;
        private const float SideInset = 0.02f;

        private readonly BoxCollider2D _body;
        private readonly LayerMask _solidLayers;

        public float HalfWidth => _body.bounds.extents.x;

        public CollisionSensor(BoxCollider2D body, LayerMask solidLayers)
        {
            _body = body;
            _solidLayers = solidLayers;
        }

        public bool IsTouchingGround()
        {
            return CastFrom(_body.bounds.center, Vector2.down, ProbeDistance);
        }

        public bool HasGroundUnderBothFeet()
        {
            Bounds bounds = _body.bounds;
            return HasGroundBelow(bounds.min.x) && HasGroundBelow(bounds.max.x);
        }

        public float FreeGroundBeyondFoot(int direction, float maxDistance)
        {
            Bounds bounds = _body.bounds;
            float footX = direction > 0 ? bounds.max.x : bounds.min.x;
            float reachableDistance = Mathf.Min(maxDistance, DistanceToWall(direction, maxDistance));
            float walkedDistance = 0f;

            while (walkedDistance + PixelGrid.UnitsPerPixel <= reachableDistance)
            {
                float nextDistance = walkedDistance + PixelGrid.UnitsPerPixel;

                if (!HasGroundBelow(footX + direction * nextDistance))
                    break;

                walkedDistance = nextDistance;
            }

            return walkedDistance;
        }

        public bool IsBlockedAbove(float upwardDistance)
        {
            return CastFrom(_body.bounds.center, Vector2.up, upwardDistance + ProbeDistance);
        }

        public bool TryFindCornerCorrection(float upwardDistance, int maxPixels, out float horizontalShift)
        {
            for (int pixels = 1; pixels <= maxPixels; pixels++)
            {
                float offset = pixels * PixelGrid.UnitsPerPixel;

                if (IsClearAboveWithShift(offset, upwardDistance))
                {
                    horizontalShift = offset;
                    return true;
                }

                if (IsClearAboveWithShift(-offset, upwardDistance))
                {
                    horizontalShift = -offset;
                    return true;
                }
            }

            horizontalShift = 0f;
            return false;
        }

        private bool HasGroundBelow(float x)
        {
            Vector2 origin = new(x, _body.bounds.min.y);
            return Physics2D.Raycast(origin, Vector2.down, ProbeDistance * 2f, _solidLayers);
        }

        private float DistanceToWall(int direction, float maxDistance)
        {
            Bounds bounds = _body.bounds;
            Vector2 raisedSize = (Vector2)bounds.size - Vector2.up * (ProbeDistance * 2f);
            RaycastHit2D hit = Physics2D.BoxCast(bounds.center, raisedSize, 0f, Vector2.right * direction, maxDistance, _solidLayers);
            return hit ? hit.distance : maxDistance;
        }

        private bool IsClearAboveWithShift(float horizontalOffset, float upwardDistance)
        {
            Vector2 shiftedCenter = (Vector2)_body.bounds.center + Vector2.right * horizontalOffset;
            return !CastFrom(shiftedCenter, Vector2.up, upwardDistance + ProbeDistance);
        }

        private bool CastFrom(Vector2 center, Vector2 direction, float distance)
        {
            Vector2 insetSize = (Vector2)_body.bounds.size - Vector2.right * (SideInset * 2f);
            return Physics2D.BoxCast(center, insetSize, 0f, direction, distance, _solidLayers);
        }
    }
}
