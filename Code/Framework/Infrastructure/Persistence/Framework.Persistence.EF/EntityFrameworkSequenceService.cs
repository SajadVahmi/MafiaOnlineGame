using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Framework.Persistence.EF;

public interface IEntityFrameworkSequenceService{
    public Task<string?> Next(string sequenceName);
}

public class EntityFrameworkSequenceService(FrameworkDbContext dbContext) : IEntityFrameworkSequenceService
{
    public async Task<string?> Next(string sequenceName)
    {
        var sequenceSqlParameter = new SqlParameter("@sequenceNumber", SqlDbType.BigInt)
        {
            Direction = ParameterDirection.Output
        };

        await dbContext.Database.ExecuteSqlRawAsync($"set @sequenceNumber = NEXT VALUE FOR {sequenceName}", sequenceSqlParameter);

        return sequenceSqlParameter.Value.ToString();
    }
}
