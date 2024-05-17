using Framework.Persistence.EventStore.Mappings.Filters;

namespace Framework.Persistence.EventStore.Mappings;

public interface ISchemaMapping
{
    IFilter CreateFilter();
}