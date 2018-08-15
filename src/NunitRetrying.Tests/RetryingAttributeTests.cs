namespace NunitRetrying.Tests
{
    using FluentAssertions;
    using NunitRetrying.Tests.Runner;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;

    public class RetryingAttributeTests
    {
        [Test]
        public void Test_TestSucceedsWhenSingleRetryHasNoAssertionFailures()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.failsAssertionTwoTimesAndRetriesTwoTimes());

            result.ResultState.Should().Be(ResultState.Failure);
            result.Output.Should().Contain("Test retried 2 time/s.");
        }

        [Test]
        public void Test_TestResultIsFailureWhenLastRetryFailsAssertion()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.failsAssertionTwoTimesAndRetriesOneTime());

            result.ResultState.Should().Be(ResultState.Failure);
            result.Output.Should().Contain("Test retried 1 time/s.");
        }

        [Test]
        public void Test_TestSucceedsWhenSingleRetryThrowsNoUnhandledExceptions()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.throwsExceptionTwoTimesAndRetriesTwoTimes());

            result.ResultState.Should().Be(ResultState.Error);
            result.Output.Should().Contain("Test retried 2 time/s.");
        }

        [Test]
        public void Test_TestResultIsErrorWhenLastRetryThrowsException()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.throwsExceptionTwoTimesAndRetriesOneTime());

            result.ResultState.Should().Be(ResultState.Error);
            result.Output.Should().Contain("Test retried 1 time/s.");
        }

        [Test]
        public void Test_TestResultIsPassedWhenLastRetryIsPassed()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.throwsExceptionTwoTimesAndRetriesThreeTimes());

            result.ResultState.Should().Be(ResultState.Success);
            result.Output.Should().Contain("Test retried 2 time/s.");
        }

        [Test]
        public void Test_TestResultIsPassedWhenRetryisGetFromConfigFile()
        {
            var result = TestRunner<RetryingSystemUnderTests>.Run(fixture =>
                fixture.throwsExceptionTwoTimesAndGetsRetriesFromConfigFile());

            result.ResultState.Should().Be(ResultState.Success);
            result.Output.Should().Contain($"Test retried 2 time/s.");
        }
    }
}