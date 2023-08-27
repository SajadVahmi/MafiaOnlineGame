using System.Reflection;

namespace Framework.Configuration
{
    public interface IDependencyRegister
    {
        void RegisterCommandHandlers(Assembly assembly);
        void RegisterQueryHandlers(Assembly assembly);
        void RegisterScoped<TService>(Func<TService> factory, Action<TService>? release = null) where TService : notnull;
        void RegisterScoped<TService, TImplementation>() where TImplementation : notnull, TService;
        void RegisterSingleton<TService>(Func<TService> factory, Action<TService>? release = null) where TService : notnull;
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
        void RegisterSingleton<TService, TInstance>(TInstance instance) where TService : class where TInstance : TService;
        void RegisterTransient<TService, TImplementation>() where TImplementation : TService;

    }
}
