namespace Framework.Core.Contracts;

public interface IMapperAdapter
{
    public TDestination Map<TSource, TDestination>(TSource source);

}