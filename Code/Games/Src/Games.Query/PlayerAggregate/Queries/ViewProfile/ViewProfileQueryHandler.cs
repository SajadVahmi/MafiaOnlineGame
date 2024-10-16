using Framework.Core.Application.Queries;
using Framework.Core.Contracts;
using Games.Query._Shared.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Games.Query.PlayerAggregate.Queries.ViewProfile;

public class ViewProfileQueryHandler(IAuthenticatedUser authenticatedUser, GamesQueryDbContext dbContext) : IQueryHandler<ViewProfileQuery, ViewProfileQueryResult?>
{
    public Task<ViewProfileQueryResult?> HandleAsync(ViewProfileQuery request, CancellationToken cancellationToken = default)
    {
        var userId = authenticatedUser.GetSub();

        return dbContext.Players
            .Where(player => player.UserId == userId && player.UserId == userId)
            .Select(player => new ViewProfileQueryResult()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Gender = player.Gender
            }).FirstOrDefaultAsync(cancellationToken);
    }
}