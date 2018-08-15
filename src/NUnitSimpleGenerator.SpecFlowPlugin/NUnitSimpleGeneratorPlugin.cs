using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Infrastructure;

[assembly: GeneratorPlugin(typeof(NUnitSimpleGenerator.SpecFlowPlugin.NUnitSimpleGeneratorPlugin))]

namespace NUnitSimpleGenerator.SpecFlowPlugin
{
    public class NUnitSimpleGeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters)
        {
            generatorPluginEvents.CustomizeDependencies += (sender, args) =>
              {
                  args.ObjectContainer
                      .RegisterTypeAs<NUnitSimpleGeneratorProvider, IUnitTestGeneratorProvider>();
              };
        }
    }
}
