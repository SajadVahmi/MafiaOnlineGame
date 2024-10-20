using System.Reflection;
using Framework.Persistence.EventStore.Mappings.Filters;

namespace Framework.Persistence.EventStore.Mappings;

public static class SchemaMappingRegistration
{
    private static Dictionary<Type, IFilter> _filters = new Dictionary<Type, IFilter>();
    public static void RegisterMappingsInAssembly(Assembly assembly)
    {
        //TODO: check this
        _filters = assembly.GetTypes()
            .Where(a => a.BaseType == typeof(SchemaMapping<>))
            .Select(Activator.CreateInstance)
            .OfType<ISchemaMapping>()
            .ToDictionary(a=> a.GetType().GetGenericTypeDefinition(), a=> a.CreateFilter());    
    }
    public static IFilter? GetFilterForType(Type type)
    {
        if (_filters.ContainsKey(type)) return _filters[type];
        return null;
    }
}