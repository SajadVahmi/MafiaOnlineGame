namespace Framework.Configuration;

public interface IFrameworkModule
{
    void Register(IDependencyRegister dependencyRegister);
}