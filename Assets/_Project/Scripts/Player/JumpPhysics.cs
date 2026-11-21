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
    }
}
