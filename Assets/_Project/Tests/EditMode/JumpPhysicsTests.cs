using JackODrain.Player;
using NUnit.Framework;

namespace JackODrain.Tests
{
    public sealed class JumpPhysicsTests
    {
        private const float Tolerance = 0.0001f;

        [TestCase(3f, 0.35f)]
        [TestCase(5f, 0.5f)]
        [TestCase(1f, 0.2f)]
        public void LaunchSpeedAndGravityReachTheRequestedHeight(float jumpHeight, float timeToApex)
        {
            float gravity = JumpPhysics.GravityFor(jumpHeight, timeToApex);
            float launchSpeed = JumpPhysics.LaunchSpeedFor(jumpHeight, timeToApex);

            float apexHeight = launchSpeed * launchSpeed / (2f * gravity);

            Assert.AreEqual(jumpHeight, apexHeight, Tolerance);
        }

        [TestCase(3f, 0.35f)]
        [TestCase(5f, 0.5f)]
        public void LaunchSpeedAndGravityReachTheApexOnTime(float jumpHeight, float timeToApex)
        {
            float gravity = JumpPhysics.GravityFor(jumpHeight, timeToApex);
            float launchSpeed = JumpPhysics.LaunchSpeedFor(jumpHeight, timeToApex);

            float apexTime = launchSpeed / gravity;

            Assert.AreEqual(timeToApex, apexTime, Tolerance);
        }

        [TestCase(2f, 48.98f)]
        [TestCase(1f, 30f)]
        public void LaunchUnderExistingGravityReachesTheRequestedHeight(float jumpHeight, float gravity)
        {
            float launchSpeed = JumpPhysics.LaunchSpeedUnderGravity(jumpHeight, gravity);

            float apexHeight = launchSpeed * launchSpeed / (2f * gravity);

            Assert.AreEqual(jumpHeight, apexHeight, Tolerance);
        }
    }
}
