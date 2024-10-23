using Framework.Core.Domain.Queries;
using Games.Contract._Shared.Enums;

namespace Games.Query.PlayerAggregate.Queries.ViewProfile;

public class ViewProfileQuery:IQuery<ViewProfileQueryResult?>
{
}

public class ViewProfileQueryResult
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
}