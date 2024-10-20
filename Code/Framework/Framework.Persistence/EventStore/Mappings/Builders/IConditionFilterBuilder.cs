namespace Framework.Persistence.EventStore.Mappings.Builders;

public interface IConditionFilterBuilder
{
    IOperationFilterBuilder WhenAbsent(string propertyName);
}