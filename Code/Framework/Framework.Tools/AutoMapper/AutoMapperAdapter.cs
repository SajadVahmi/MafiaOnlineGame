using AutoMapper;
using Framework.Core.ServiceContracts;

namespace Framework.Tools.AutoMapper;

public class AutoMapperAdapter(IMapper mapper) : IMapperAdapter
{
    public TDestination Map<TDestination>(object source)
    {
        return mapper.Map<TDestination>(source);
    }
}