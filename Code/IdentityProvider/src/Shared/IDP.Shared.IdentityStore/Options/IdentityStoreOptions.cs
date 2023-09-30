namespace IDP.Shared.IdentityStore.Options
{
    public class IdentityStoreOptions
    {
        public string? ConnectionString { get; set; }
        public string? MigrationsAssembly { get; set; }
        public string? MigrationsHistoryTable { get; set; }
        public string? Schema { get; set; }
    }
}
