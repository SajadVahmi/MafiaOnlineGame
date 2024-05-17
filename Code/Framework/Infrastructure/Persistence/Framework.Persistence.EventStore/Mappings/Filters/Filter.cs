using Framework.Persistence.EventStore.Mappings.Conditions;
using Framework.Persistence.EventStore.Mappings.Operation;
using Newtonsoft.Json.Linq;

namespace Framework.Persistence.EventStore.Mappings.Filters;

internal class Filter : IFilter
{
    private readonly ICondition _condition;
    private readonly IOperation _operation;
    private IFilter _nextFilter;
    internal Filter(ICondition condition, IOperation operation)
    {
        _condition = condition;
        _operation = operation;
        _nextFilter = EndFilter.Instance;
    }
    public void SetNext(IFilter next)
    {
        this._nextFilter = next;
    }
    public JObject Apply(JObject json)
    {
        if (_condition.IsSatisfied(json))
            json = _operation.Apply(json);

        return _nextFilter.Apply(json);
    }
}