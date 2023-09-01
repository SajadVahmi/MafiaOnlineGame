using Framework.Configuration;
using Framework.Core.Domian.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.EntityFramework.Commands;

public class EntityFrameworkModule : IFrameworkModule
{
    public void Register(IDependencyRegister dependencyRegister)
    {
        dependencyRegister.RegisterScoped<IDbContextTransaction, RelationalTransaction>();
        dependencyRegister.RegisterScoped<IUnitOfWork, EfUnitOfWork>();
    }
}