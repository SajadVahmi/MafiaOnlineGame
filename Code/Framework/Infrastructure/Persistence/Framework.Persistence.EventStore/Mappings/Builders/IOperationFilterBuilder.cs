namespace Framework.Persistence.EventStore.Mappings.Builders;

public interface IOperationFilterBuilder
{
    IConditionFilterBuilder SetDefaultValue(string value);
    IConditionFilterBuilder ThrowError(string errorMessage);
    IConditionFilterBuilder ThrowError();
    IConditionFilterBuilder PickValueFromAnotherProperty(string propertyName);
}