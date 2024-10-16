namespace Framework.Core.Contracts;

public interface IMapperAdapter
{
    public TDestination Map<TDestination>(object source);
}