using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Framework.Persistence.EF;

public class SequenceHelper
{
    private readonly FrameworkDbContext _dbContext;

    public SequenceHelper(FrameworkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<long> Next(string sequenceName)
    {
        var sequenceSqlParameter = new SqlParameter("@sequenceNumber", SqlDbType.BigInt)
        {
            Direction = ParameterDirection.Output
        };

        await _dbContext.Database.ExecuteSqlRawAsync($"set @sequenceNumber = NEXT VALUE FOR {sequenceName}", sequenceSqlParameter);

        return (long)sequenceSqlParameter.Value;
    }
}
