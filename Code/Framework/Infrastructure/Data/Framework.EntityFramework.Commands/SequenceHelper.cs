using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Framework.EntityFramework.Commands
{
    public class SequenceHelper
    {
        private readonly CommandDbContext _dbContext;

        public SequenceHelper(CommandDbContext dbContext)
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
}
