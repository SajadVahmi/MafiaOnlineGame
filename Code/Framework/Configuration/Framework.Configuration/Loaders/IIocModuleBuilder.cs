namespace Framework.Configuration.Loaders
{
    public interface IIocModuleBuilder
    {
        IModuleBuilder WithIocModule(IFrameworkIocModule module);
    }
}