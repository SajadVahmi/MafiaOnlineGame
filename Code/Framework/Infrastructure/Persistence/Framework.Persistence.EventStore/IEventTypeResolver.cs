using System.Reflection;

namespace Framework.Persistence.EventStore;

public interface IEventTypeResolver
{
    void AddTypesFromAssembly(Assembly assembly);
    Type GetType(string typeName);
}