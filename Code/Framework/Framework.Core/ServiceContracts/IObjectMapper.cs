namespace Framework.Core.ServiceContracts;

public interface IMapperAdapter
{
    public TDestination Map<TDestination>(object source);
}