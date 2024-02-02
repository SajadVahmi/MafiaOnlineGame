namespace Framework.Presentation.RestApi.Versioning;

public class VersioningOptions
{
    public bool? AssumeDefaultVersionWhenUnspecified { get; set; }
    public DefaultApiVersion? DefaultApiVersion { get; set; }
    public string? GroupNameFormat { get; set; }
    public bool? SubstituteApiVersionInUrl { get; set; }
}

public class DefaultApiVersion
{
    public int? Major { get; set; }
    public int? Minor { get; set; }
}