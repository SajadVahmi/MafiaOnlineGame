﻿namespace IDP.STS.OperationalStore.Options;

public class OperationalStoreOptions
{
    public string? ConnectionString { get; set; }
    public string? MigrationsAssembly { get; set; }
    public string? MigrationsHistoryTable { get; set; }
    public string? Schema { get; set; }
}