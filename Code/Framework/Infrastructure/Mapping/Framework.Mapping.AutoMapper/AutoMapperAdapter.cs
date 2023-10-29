using AutoMapper;
using Framework.Core.Contracts;

namespace Framework.Mapping.AutoMapper;

public class AutoMapperAdapter : IMapperAdapter
{
    private readonly IMapper _mapper;

    public AutoMapperAdapter(IMapper mapper)
    {
        _mapper = mapper;
    }
    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
}
