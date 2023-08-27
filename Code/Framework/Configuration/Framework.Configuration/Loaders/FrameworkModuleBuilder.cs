namespace Framework.Configuration.Loaders
{
    public class FrameworkModuleBuilder : IIocModuleBuilder, IModuleBuilder
    {
        private FrameworkModuleBuilder() { }
        public static IIocModuleBuilder Setup()
        {
            return new FrameworkModuleBuilder();
        }
        public IModuleBuilder WithModule(IFrameworkModule module)
        {
            FrameworkModuleRegistry.Install(module);
            return this;
        }

        public IModuleBuilder WithModule<T>() where T : IFrameworkModule, new()
        {
            FrameworkModuleRegistry.Install<T>();
            return this;
        }

        public IModuleBuilder WithIocModule(IFrameworkIocModule module)
        {
            FrameworkModuleRegistry.Install(module);
            FrameworkModuleRegistry.Install<CoreModule>();
            return this;
        }
    }
}
