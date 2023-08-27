namespace Framework.Configuration.Loaders
{
    public interface IModuleBuilder
    {
        IModuleBuilder WithModule(IFrameworkModule module);
        IModuleBuilder WithModule<T>() where T : IFrameworkModule, new();
    }
}
