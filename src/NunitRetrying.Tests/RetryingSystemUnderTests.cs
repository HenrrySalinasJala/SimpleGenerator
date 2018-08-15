using System;
using NUnit.Framework;

namespace NunitRetrying.Tests
{
    [Explicit("These tests should only run programmatically by other tests or for debugging purposes.")]
    public class RetryingSystemUnderTests
    {
        private int _twoTimesToFail = 2;

        private int _threeTimesToFail = 3;

        [Test]
        [Retrying(Times = 2)]
        public void failsAssertionTwoTimesAndRetriesTwoTimes()
        {
            MaybeFailAssertion();
        }

        [Test]
        [Retrying(Times = 1)]
        public void failsAssertionTwoTimesAndRetriesOneTime()
        {
            MaybeFailAssertion();
        }

        private void MaybeFailAssertion()
        {
            if (_twoTimesToFail > 0)
            {
                _twoTimesToFail--;

                Assert.Fail("welp!");
            }
        }

        [Test]
        [Retrying(Times = 2)]
        public void throwsExceptionTwoTimesAndRetriesTwoTimes()
        {
            ThrowExceptionTwoTimes();
        }

        [Test]
        [Retrying(Times = 1)]
        public void throwsExceptionTwoTimesAndRetriesOneTime()
        {
            ThrowExceptionTwoTimes();
        }


        [Test]
        [Retrying(Times = 3)]
        public void throwsExceptionTwoTimesAndRetriesThreeTimes()
        {
            ThrowExceptionTwoTimes();
        }


        [Test]
        [Retrying()]
        public void throwsExceptionTwoTimesAndGetsRetriesFromConfigFile()
        {
            ThrowExceptionTwoTimes();
        }

        private void ThrowExceptionTwoTimes()
        {
            if (_twoTimesToFail > 0)
            {
                _twoTimesToFail--;
                throw new Exception("oops!");
            }
        }

        private void ThrowExceptionThreeTimes()
        {
            if (_threeTimesToFail > 0)
            {
                _threeTimesToFail--;
                throw new Exception("oops!");
            }
        }

    }
}
