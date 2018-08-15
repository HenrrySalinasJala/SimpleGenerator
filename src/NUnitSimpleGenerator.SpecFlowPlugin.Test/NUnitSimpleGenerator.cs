using NUnit.Framework;
using NUnitRetrying.Environment;
using System;
using TechTalk.SpecFlow;

namespace NUnitSimpleGenerator.SpecFlowPlugin.Test
{
    [Binding]
    public class NUnitSimpleGenerator
    {

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {

        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {

        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            if ((TestContext.CurrentContext.CurrentRepeatCount + 1) < Configuration.RetryTimes)
            {
                throw new Exception("Unexpected error !!!");
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

    }
}
