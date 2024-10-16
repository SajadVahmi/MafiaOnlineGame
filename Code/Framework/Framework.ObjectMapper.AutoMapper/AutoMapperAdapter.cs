using AutoMapper;
using Framework.Core.Contracts;

namespace Framework.ObjectMapper.AutoMapper;

public class AutoMapperAdapter(IMapper mapper) : IMapperAdapter
{
    public TDestination Map<TDestination>(object source)
    {
        return mapper.Map<TDestination>(source);
    }
}