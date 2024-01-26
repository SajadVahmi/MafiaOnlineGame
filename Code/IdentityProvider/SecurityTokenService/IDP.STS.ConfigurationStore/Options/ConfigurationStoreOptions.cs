namespace IDP.STS.ConfigurationStore.Options;

public class ConfigurationStoreOptions
{
    public string? ConnectionString { get; set; }
    public string? MigrationsAssembly { get; set; }
    public string? MigrationsHistoryTable { get; set; }
    public string? Schema { get; set; }
}