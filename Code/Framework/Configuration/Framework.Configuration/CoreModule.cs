using System.Diagnostics.Tracing;
using Framework.Core.ApplicationServices.Commands;
using Framework.Core.ApplicationServices.Queries;

namespace Framework.Configuration
{
    public class CoreModule : IFrameworkModule
    {
        public void Register(IDependencyRegister dependencyRegister)
        {
            dependencyRegister.RegisterScoped<IQueryBus, QueryBus>();
            dependencyRegister.RegisterScoped<ICommandBus, CommandBus>();
        }
    }
}
