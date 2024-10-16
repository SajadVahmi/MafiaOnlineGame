using Framework.Core.Domain.Events;
using Framework.Persistence.EventStore.Mappings.Builders;
using Framework.Persistence.EventStore.Mappings.Filters;

namespace Framework.Persistence.EventStore.Mappings
{
    public abstract class SchemaMapping<T> : ISchemaMapping where T : IDomainEvent
    {
        public IFilter CreateFilter()
        {
            var builder = CreateFilterBuilder();
            Configure(builder);
            return builder.Build();
        }
        private static FilterBuilder CreateFilterBuilder()
        {
            return new FilterBuilder();
        }
        protected abstract void Configure(IFilterBuilder builder);
    }

}