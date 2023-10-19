using System.Reflection;

namespace Framework.Configuration;

public interface IDependencyRegister
{
    void RegisterDomainServices(Assembly assembly);

    void RegisterApplicationServices(Assembly assembly);

    void RegisterCommandHandlers(Assembly assembly);

    void RegisterQueryHandlers(Assembly assembly);

    void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService;

    void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;

    void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService;

    void RegisterTransient<TService, TImplementation>() where TImplementation : TService;

    void RegisterDecorator<TService, TDecorator>() where TDecorator : TService;

    void RegisterDecorator(Type service, Type decorator);

}