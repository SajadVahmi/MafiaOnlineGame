namespace Framework.Configuration
{
    public interface IFrameworkIocModule : IFrameworkModule
    {
        IDependencyRegister CreateServiceRegistry();
    }
}
