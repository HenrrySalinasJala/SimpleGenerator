namespace NunitRetrying.Tests.Runner
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using NUnit.Framework.Internal;
    using NUnit.Framework.Internal.Builders;

    /// <summary>
    /// NUnit Test runner wrapper for testing.
    /// </summary>
    /// <typeparam name="TFixture"></typeparam>
    public class TestFixtureWrapper<TFixture>
        where TFixture : new()
    {
        /// <summary>
        /// Test Fixture initialization.
        /// </summary>
        private readonly TFixture _fixture = new TFixture();

        /// <summary>
        /// Gets the test given the test selector.
        /// </summary>
        /// <param name="testSelector">The test selector see <see cref="Expression"/>.</param>
        /// <returns>The Wrapped test.</returns>
        public TestWrapper GetTest(Expression<Action<TFixture>> testSelector)
        {
            var testMethodName = ((MethodCallExpression) testSelector.Body).Method.Name;

            return GetTest(testMethodName);
        }

        /// <summary>
        /// Gets the test given the test name.
        /// </summary>
        /// <param name="testName">The test name.</param>
        /// <returns>The Wrapped test see <see cref="TestWrapper"/>.</returns>
        private TestWrapper GetTest(string testName)
        {
            var testSuite = GetTestSuite();

            var selectedTest = testSuite.Tests.Single(test => test.Name == testName);

            return new TestWrapper(selectedTest, _fixture);
        }

        /// <summary>
        /// Gets the test suite.
        /// </summary>
        /// <returns>The Test suite <see cref="TestSuite"/></returns>
        private TestSuite GetTestSuite()
        {
            var suiteBuilder = new DefaultSuiteBuilder();

            var testSuite = suiteBuilder.BuildFrom(new TypeWrapper(typeof(TFixture)));

            testSuite.Fixture = _fixture;

            return testSuite;
        }
    }
}