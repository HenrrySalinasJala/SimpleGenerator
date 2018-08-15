using System.CodeDom;
using System.Collections.Generic;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Utils;

namespace NUnitSimpleGenerator.SpecFlowPlugin
{
    public class NUnitSimpleGeneratorProvider : IUnitTestGeneratorProvider
    {
        private const string NUnitFrameworkNamespace = "NUnit.Framework";
        protected CodeDomHelper CodeDomHelper { get; set; }

        private readonly IUnitTestGeneratorProvider _unitTestGeneratorProvider;
        public NUnitSimpleGeneratorProvider(CodeDomHelper codeDomHelper)
        {
            _unitTestGeneratorProvider = new NUnit3TestGeneratorProvider(codeDomHelper);
            CodeDomHelper = codeDomHelper;
        }

        public UnitTestGeneratorTraits GetTraits()
        {
            return _unitTestGeneratorProvider.GetTraits();
        }

        public void SetTestClass(TestClassGenerationContext generationContext, string featureTitle, string featureDescription)
        {
            _unitTestGeneratorProvider.SetTestClass(generationContext, featureTitle, featureDescription);

            UpdateAttributes(generationContext.TestClass);
        }

        public void SetTestClassCategories(TestClassGenerationContext generationContext, IEnumerable<string> featureCategories)
        {
            _unitTestGeneratorProvider.SetTestClassCategories(generationContext, featureCategories);

            UpdateAttributes(generationContext.TestClass);
        }

        public void SetTestClassIgnore(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.SetTestClassIgnore(generationContext);

            UpdateAttributes(generationContext.TestClass);
        }

        public void FinalizeTestClass(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.FinalizeTestClass(generationContext);

            UpdateAttributes(generationContext.TestClass);
        }

        public void SetTestClassParallelize(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.SetTestClassParallelize(generationContext);

            UpdateAttributes(generationContext.TestClass);
        }

        public void SetTestClassInitializeMethod(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.SetTestClassInitializeMethod(generationContext);

            UpdateAttributes(generationContext.TestClassInitializeMethod);
        }

        public void SetTestClassCleanupMethod(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.SetTestClassCleanupMethod(generationContext);

            UpdateAttributes(generationContext.TestClassCleanupMethod);
        }

        public void SetTestInitializeMethod(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.SetTestInitializeMethod(generationContext);

            UpdateAttributes(generationContext.TestInitializeMethod);
        }

        public void SetTestCleanupMethod(TestClassGenerationContext generationContext)
        {
            _unitTestGeneratorProvider.SetTestCleanupMethod(generationContext);

            UpdateAttributes(generationContext.TestCleanupMethod);
        }

        public void SetTestMethod(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string friendlyTestName)
        {
            _unitTestGeneratorProvider.SetTestMethod(generationContext, testMethod, friendlyTestName);

            CodeDomHelper.AddAttribute(testMethod, "NUnit.Framework.Retrying");
            UpdateAttributes(testMethod);
        }

        public void SetTestMethodCategories(TestClassGenerationContext generationContext, CodeMemberMethod testMethod,
            IEnumerable<string> scenarioCategories)
        {
            _unitTestGeneratorProvider.SetTestMethodCategories(generationContext, testMethod, scenarioCategories);

            UpdateAttributes(testMethod);
        }

        public void SetTestMethodIgnore(TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            _unitTestGeneratorProvider.SetTestMethodIgnore(generationContext, testMethod);

            UpdateAttributes(testMethod);
        }

        public void SetRowTest(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string scenarioTitle)
        {
            _unitTestGeneratorProvider.SetRowTest(generationContext, testMethod, scenarioTitle);

            UpdateAttributes(testMethod);
        }

        public void SetRow(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, IEnumerable<string> arguments,
            IEnumerable<string> tags, bool isIgnored)
        {
            _unitTestGeneratorProvider.SetRow(generationContext, testMethod, arguments, tags, isIgnored);

            UpdateAttributes(testMethod);
        }

        public void SetTestMethodAsRow(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string scenarioTitle,
            string exampleSetName, string variantName, IEnumerable<KeyValuePair<string, string>> arguments)
        {
            _unitTestGeneratorProvider.SetTestMethodAsRow(generationContext, testMethod, scenarioTitle, exampleSetName,
                variantName, arguments);

            UpdateAttributes(testMethod);
        }

        private void UpdateAttributes(CodeTypeMember codeTypeMember)
        {
            foreach (CodeAttributeDeclaration codeAttribute in codeTypeMember.CustomAttributes)
            {
                if (codeAttribute.Name.StartsWith(NUnitFrameworkNamespace))
                    codeAttribute.Name = "global::" + codeAttribute.Name;
            }
        }
    }
}