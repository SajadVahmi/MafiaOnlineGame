using AutoMapper;
using Framework.Core.Contracts;

namespace Framework.Mapping.AutoMapper;

public class AutoMapperAdapter(IMapper mapper) : IMapperAdapter
{
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return mapper.Map<TDestination>(source);
    }
}
