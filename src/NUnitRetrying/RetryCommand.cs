using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitRetrying
{
    /// <summary>
    /// Retry Test Command decorator.
    /// </summary>
    public class RetryingCommand : DelegatingTestCommand
    {
        /// <summary>
        /// Times to retry.
        /// </summary>
        private readonly int _times;

        /// <summary>
        /// Initializes a new instance of <see cref="RetryingCommand"/>.
        /// </summary>
        /// <param name="innerCommand"></param>
        /// <param name="times"></param>
        public RetryingCommand(TestCommand innerCommand, int times)
            : base(innerCommand)
        {
            _times = times;
        }

        /// <summary>
        /// Executes the test.
        /// </summary>
        /// <param name="context">Current Test context.</param>
        /// <returns>The Test results.</returns>
        public override TestResult Execute(TestExecutionContext context)
        {
            int count = _times;

            while (count-- > 0)
            {
                try
                {
                    RunTest(context);
                }
                catch (Exception ex)
                {
                    if (context.CurrentResult == null)
                    {
                        context.CurrentResult = context.CurrentTest.MakeTestResult();
                    }
                    context.CurrentResult.RecordException(ex);
                }
                context.CurrentRepeatCount++;
                if (!TestFailed(context))
                {
                    break;
                }
                if (count > 0)
                {
                    context.CurrentResult = context.CurrentTest.MakeTestResult();
                }

                if (context.CurrentRepeatCount <= _times)
                {
                    context.OutWriter.WriteLine();
                    context.OutWriter.WriteLine($"Test retried {context.CurrentRepeatCount} time/s.");
                }
            }

            return context.CurrentResult;
        }

        /// <summary>
        /// Executes the given Test in the given context.s
        /// </summary>
        /// <param name="context">Current Test Context.</param>
        private void RunTest(TestExecutionContext context)
        {
            context.CurrentResult = innerCommand.Execute(context);
        }

        /// <summary>
        /// Check if the Test is failed.
        /// </summary>
        /// <param name="context">Current Test Context.</param>
        /// <returns>true if the test has failed otherwise false.</returns>
        private static bool TestFailed(TestExecutionContext context)
        {
            return UnsuccessfulResultStates.Contains(context.CurrentResult.ResultState);
        }

        /// <summary>
        /// Possible states of a failed Test.
        /// </summary>
        private static ResultState[] UnsuccessfulResultStates => new[]
        {
                ResultState.Failure,
                ResultState.Error,
                ResultState.Cancelled,
                ResultState.SetUpError,
                ResultState.SetUpFailure,
        };
    }
}
