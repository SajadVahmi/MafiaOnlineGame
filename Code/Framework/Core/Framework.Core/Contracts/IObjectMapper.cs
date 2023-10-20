namespace Framework.Core.Contracts;

public interface IObjectMapper
{
   public TDestination Map<TSource, TDestination>(TSource source);
   
}