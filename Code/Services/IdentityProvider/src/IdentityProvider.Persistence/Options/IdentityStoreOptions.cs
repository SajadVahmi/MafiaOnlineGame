namespace IdentityProvider.Persistence.Options;

public class IdentityStoreOptions
{
    public string? ConnectionString { get; set; }
    public string? MigrationsHistoryTable { get; set; }
    public string? Schema { get; set; }
}