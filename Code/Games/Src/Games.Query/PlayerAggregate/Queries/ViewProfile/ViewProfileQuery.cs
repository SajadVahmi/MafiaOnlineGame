using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Core.Application.Queries;
using Games.Domain.Contracts.Enums;

namespace Games.Query.PlayerAggregate.Queries.ViewProfile;

public class ViewProfileQuery:IQuery<ViewProfileQueryResult?>
{

}

public class ViewProfileQueryResult
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Gender? Gender { get; set; }
}