using UnityEngine;

namespace JackODrain.Player
{
    public static class SafeGround
    {
        public static float StandingPointAwayFromEdges(float centerX, float halfWidth, float freeLeft, float freeRight, float edgeMargin)
        {
            float leftEdge = centerX - halfWidth - freeLeft;
            float rightEdge = centerX + halfWidth + freeRight;

            float lowestSafeCenter = leftEdge + halfWidth + edgeMargin;
            float highestSafeCenter = rightEdge - halfWidth - edgeMargin;

            if (lowestSafeCenter > highestSafeCenter)
                return (leftEdge + rightEdge) / 2f;

            return Mathf.Clamp(centerX, lowestSafeCenter, highestSafeCenter);
        }
    }
}
