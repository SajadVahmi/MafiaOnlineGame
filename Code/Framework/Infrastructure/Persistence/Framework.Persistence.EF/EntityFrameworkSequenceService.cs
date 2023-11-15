using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Framework.Persistence.EF;

public interface IEntityFrameworkSequenceService{
    public Task<string?> Next(string sequenceName);
}

public class EntityFrameworkSequenceService: IEntityFrameworkSequenceService
{
    private readonly FrameworkDbContext _dbContext;

    public EntityFrameworkSequenceService(FrameworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string?> Next(string sequenceName)
    {
        var sequenceSqlParameter = new SqlParameter("@sequenceNumber", SqlDbType.BigInt)
        {
            Direction = ParameterDirection.Output
        };

        await _dbContext.Database.ExecuteSqlRawAsync($"set @sequenceNumber = NEXT VALUE FOR {sequenceName}", sequenceSqlParameter);

        return sequenceSqlParameter.Value.ToString();
    }
}
