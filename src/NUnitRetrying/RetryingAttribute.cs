namespace NUnit.Framework
{
    using System;
    using System.Linq;
    using NUnit.Framework.Interfaces;
    using NUnit.Framework.Internal;
    using NUnit.Framework.Internal.Commands;
    using NUnitRetrying;
    using NUnitRetrying.Environment;

    /// <summary>
    /// NUnit Retry Attribute custom decorator.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RetryingAttribute : Attribute, IWrapSetUpTearDown
    {
        /// <summary>
        /// Times to Retry.
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="RetryingAttribute"/>.
        /// </summary>
        /// <param name="times">Times to retry</param>
        public RetryingAttribute(int times = 1)
        {
            Times = times;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RetryingAttribute"/>.
        /// </summary>
        public RetryingAttribute()
        {
            Times = Configuration.RetryTimes;
        }


        /// <summary>
        /// Wraps the test command.
        /// </summary>
        /// <param name="command">test command.</param>
        /// <returns>instance of <see cref="TestCommand"/>.</returns>
        public TestCommand Wrap(TestCommand command)
        {
            return new RetryingCommand(command, Times);
        }
    }
}
