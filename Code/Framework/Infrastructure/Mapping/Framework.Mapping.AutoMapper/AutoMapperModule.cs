using AutoMapper;
using Framework.Configuration;
using Framework.Core.Contracts;
using System.Reflection;

namespace Framework.Mapping.AutoMapper;

public class AutoMapperModule(params Assembly[] assemblies) : IFrameworkModule
{
    public void Register(IDependencyRegister dependencyRegister)
    {

        var autoMapperConfiguration = new MapperConfiguration(cfg => {

            cfg.AddMaps(assemblies);

        });

        var mapper = new Mapper(autoMapperConfiguration);

        dependencyRegister.RegisterSingleton<IMapperAdapter>(()=>new AutoMapperAdapter(mapper));

        
    }
}
