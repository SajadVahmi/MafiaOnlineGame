using AutoMapper;
using Framework.Configuration;
using Framework.Core.Contracts;
using System.Reflection;

namespace Framework.Mapping.AutoMapper;

public class AutoMapperModule : IFrameworkModule
{
    private readonly Assembly[] _assemblies;

    public AutoMapperModule(params Assembly[] assemblies)
    {
        _assemblies = assemblies;
    }
    public void Register(IDependencyRegister dependencyRegister)
    {

        var autoMapperConfiguration = new MapperConfiguration(cfg => {
            cfg.AddMaps(_assemblies);
        });

        var mapper = new Mapper(autoMapperConfiguration);

        dependencyRegister.RegisterSingleton<IMapperAdapter>(()=>new AutoMapperAdapter(mapper));

        
    }
}
