using JackODrain.Player;
using NUnit.Framework;

namespace JackODrain.Tests
{
    public sealed class HealthPoolTests
    {
        private const int MaxHearts = 5;
        private const float InvulnerabilitySeconds = 1f;

        private HealthPool _pool;

        [SetUp]
        public void CreatePool()
        {
            _pool = new HealthPool(MaxHearts, InvulnerabilitySeconds);
        }

        [Test]
        public void StartsWithAllHearts()
        {
            Assert.AreEqual(MaxHearts, _pool.Hearts);
        }

        [Test]
        public void HitRemovesOneHeart()
        {
            HitResult result = _pool.TakeHit();

            Assert.AreEqual(HitResult.Hurt, result);
            Assert.AreEqual(MaxHearts - 1, _pool.Hearts);
        }

        [Test]
        public void HitDuringInvulnerabilityIsIgnored()
        {
            _pool.TakeHit();

            HitResult result = _pool.TakeHit();

            Assert.AreEqual(HitResult.Ignored, result);
            Assert.AreEqual(MaxHearts - 1, _pool.Hearts);
        }

        [Test]
        public void HitAfterInvulnerabilityEndsCounts()
        {
            _pool.TakeHit();
            _pool.Tick(InvulnerabilitySeconds);

            _pool.TakeHit();

            Assert.AreEqual(MaxHearts - 2, _pool.Hearts);
        }

        [Test]
        public void LastHeartDepletesThePool()
        {
            HitResult result = HitResult.Ignored;

            for (int hit = 0; hit < MaxHearts; hit++)
            {
                result = _pool.TakeHit();
                _pool.Tick(InvulnerabilitySeconds);
            }

            Assert.AreEqual(HitResult.Depleted, result);
            Assert.AreEqual(0, _pool.Hearts);
        }

        [Test]
        public void RefillRestoresAllHearts()
        {
            _pool.TakeHit();

            _pool.Refill();

            Assert.AreEqual(MaxHearts, _pool.Hearts);
        }
    }
}
