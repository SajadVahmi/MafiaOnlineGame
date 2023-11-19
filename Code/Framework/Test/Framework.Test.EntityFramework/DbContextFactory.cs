using Microsoft.EntityFrameworkCore;

namespace Framework.Test.EntityFramework;

public static class DbContextFactory<TDbContext> where TDbContext : DbContext
{
    public static TDbContext Create(DbContextOptions dbContextOptions)
    {
        var dbContext = Activator.CreateInstance(typeof(TDbContext), args: dbContextOptions);

        if (dbContext is null)
            throw new Exception("Cannot create dbcontext instance");

        return (TDbContext)dbContext;
    }
}
