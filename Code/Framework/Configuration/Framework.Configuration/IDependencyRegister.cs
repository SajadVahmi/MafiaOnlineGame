using System.Reflection;

namespace Framework.Configuration;

public interface IDependencyRegister
{
    void RegisterDomainServices(Assembly assembly);

    void RegisterApplicationServices(Assembly assembly);

    void RegisterCommandHandlers(Assembly assembly);

    void RegisterQueryHandlers(Assembly assembly);

    void RegisterRepositories(Assembly assembly);

    void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull;

    void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class;

    void RegisterSingleton<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull;

    void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService;

    void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : class;

    void RegisterTransient<TService, TImplementation>() where TImplementation : notnull, TService where TService : notnull;

    void RegisterDecorator<TService, TDecorator>() where TDecorator : notnull, TService where TService : notnull;

    void RegisterDecorator(Type service, Type decorator);

}