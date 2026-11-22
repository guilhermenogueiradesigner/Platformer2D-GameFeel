using JackODrain.Player;
using NUnit.Framework;

namespace JackODrain.Tests
{
    public sealed class SafeGroundTests
    {
        private const float HalfWidth = 0.4f;
        private const float EdgeMargin = 0.5f;
        private const float WideGround = 2f;
        private const float Tolerance = 0.0001f;

        [Test]
        public void StaysPutWhenFarFromBothEdges()
        {
            float safeX = SafeGround.StandingPointAwayFromEdges(10f, HalfWidth, WideGround, WideGround, EdgeMargin);

            Assert.AreEqual(10f, safeX, Tolerance);
        }

        [Test]
        public void MovesAwayFromALeftEdgeThatIsTooClose()
        {
            float safeX = SafeGround.StandingPointAwayFromEdges(10f, HalfWidth, 0.1f, WideGround, EdgeMargin);

            Assert.AreEqual(10.4f, safeX, Tolerance);
        }

        [Test]
        public void MovesAwayFromARightEdgeThatIsTooClose()
        {
            float safeX = SafeGround.StandingPointAwayFromEdges(10f, HalfWidth, WideGround, 0f, EdgeMargin);

            Assert.AreEqual(9.5f, safeX, Tolerance);
        }

        [Test]
        public void UsesTheCenterOfAPlatformNarrowerThanTheMargins()
        {
            float safeX = SafeGround.StandingPointAwayFromEdges(10f, HalfWidth, 0f, 0.2f, EdgeMargin);

            Assert.AreEqual(10.1f, safeX, Tolerance);
        }
    }
}
