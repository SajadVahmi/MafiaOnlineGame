namespace Framework.Configuration.Loaders;

internal static class FrameworkModuleRegistry
{
    public static void Install<T>(T module) where T : IFrameworkModule
    {
        if (module is IFrameworkIocModule ioc) DependencyRegistry.SetCurrent(ioc.CreateServiceRegistry());

        var serviceRegistry = DependencyRegistry.Current;
        module.Register(serviceRegistry);
    }

    public static void Install<T>() where T : IFrameworkModule, new()
    {
        var module = new T();
        Install(module);
    }
}