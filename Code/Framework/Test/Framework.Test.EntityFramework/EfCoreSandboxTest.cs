using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Framework.Test.EntityFramework;

public class EfCoreSandboxTest<TDbContext> : IDisposable where TDbContext : DbContext
{
    public TDbContext DbContext;

    public EfCoreSandboxTest(DbContextOptions dbContextOptions)
    {
        DbContext = DbContextFactory<TDbContext>.Create(dbContextOptions);

        DbContext.Database.EnsureCreated();

    }
    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();

        DbContext.Dispose();
    }
    public static string RandomConnectionString(string baseConnection)
    {
        var builder = new SqlConnectionStringBuilder(baseConnection);

        builder.InitialCatalog = $"{builder.InitialCatalog}_{Guid.NewGuid():N}";

        return builder.ConnectionString;
    }
}
