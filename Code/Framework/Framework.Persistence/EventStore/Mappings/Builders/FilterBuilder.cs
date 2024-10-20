using Framework.Persistence.EventStore.Mappings.Conditions;
using Framework.Persistence.EventStore.Mappings.Filters;
using Framework.Persistence.EventStore.Mappings.Operation;

namespace Framework.Persistence.EventStore.Mappings.Builders;

public class FilterBuilder : IFilterBuilder, IOperationFilterBuilder
{
    private readonly List<IFilter> _filters = new List<IFilter>();
    private ICondition _currentCondition;
    public IOperationFilterBuilder WhenAbsent(string propertyName)
    {
        _currentCondition = new AbsentCondition(propertyName);
        return this;
    }

    public IConditionFilterBuilder SetDefaultValue(string value)
    {
        return AddFilter(new DefaultValueOperation(_currentCondition.PropertyName, value));
    }
    public IConditionFilterBuilder ThrowError(string errorMessage)
    {
        return AddFilter(new ErrorOperation(errorMessage));
    }
    public IConditionFilterBuilder ThrowError()
    {
        return AddFilter(new ErrorOperation());
    }

    public IConditionFilterBuilder PickValueFromAnotherProperty(string propertyName)
    {
        return AddFilter(new AnotherPropertyOperation(_currentCondition.PropertyName, propertyName));
    }
    private IConditionFilterBuilder AddFilter(IOperation operation)
    {
        var filter = new Filter(_currentCondition, operation);
        _filters.Add(filter);
        return this;
    }
    public IFilter Build()
    {
        if (!_filters.Any()) return EndFilter.Instance;

        this._filters.Aggregate((a, b) =>
        {
            a.SetNext(b);
            return b;
        });
        return _filters.First();
    }
}