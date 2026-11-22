using UnityEngine;

namespace JackODrain.Player
{
    public static class JumpPhysics
    {
        public static float GravityFor(float jumpHeight, float timeToApex)
        {
            return 2f * jumpHeight / (timeToApex * timeToApex);
        }

        public static float LaunchSpeedFor(float jumpHeight, float timeToApex)
        {
            return 2f * jumpHeight / timeToApex;
        }

        public static float LaunchSpeedUnderGravity(float jumpHeight, float gravity)
        {
            return Mathf.Sqrt(2f * gravity * jumpHeight);
        }
    }
}
