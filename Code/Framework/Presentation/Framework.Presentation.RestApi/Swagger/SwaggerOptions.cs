namespace Framework.Presentation.RestApi.Swagger;

public class SwaggerOptions
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public SwaggerOptionsContact? Contact  { get; set; } 
    public string? IsDeprecatedApiDescription { get; set; }
    public SwaggerSecurityScheme? SecurityScheme { get; set; }

}

public class SwaggerSecurityScheme
{
    public string? Name { get; set; }

    public string? AuthorizationUrl { get; set; }
    public string? TokenUrl { get; set; }

    public Dictionary<string,string>? Scopes { get; set; }

}
public class SwaggerOptionsContact
{
    public string? Name { get; set; }

    public string? Email { get; set; }
}