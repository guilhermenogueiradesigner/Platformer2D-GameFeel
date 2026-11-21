using JackODrain.Core;
using NUnit.Framework;

namespace JackODrain.Tests
{
    public sealed class CountdownTests
    {
        [Test]
        public void IsNotRunningBeforeStart()
        {
            var countdown = new Countdown();

            Assert.IsFalse(countdown.IsRunning);
        }

        [Test]
        public void RunsUntilDurationElapses()
        {
            var countdown = new Countdown();
            countdown.Start(0.1f);

            countdown.Tick(0.05f);
            Assert.IsTrue(countdown.IsRunning);

            countdown.Tick(0.05f);
            Assert.IsFalse(countdown.IsRunning);
        }

        [Test]
        public void StopEndsImmediately()
        {
            var countdown = new Countdown();
            countdown.Start(1f);

            countdown.Stop();

            Assert.IsFalse(countdown.IsRunning);
        }

        [Test]
        public void NegativeDurationDoesNotRun()
        {
            var countdown = new Countdown();

            countdown.Start(-1f);

            Assert.IsFalse(countdown.IsRunning);
        }
    }
}
