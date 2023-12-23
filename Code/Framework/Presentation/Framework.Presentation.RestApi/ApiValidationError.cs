using FluentValidation.Results;

namespace Framework.Presentation.RestApi;

public class ApiValidationError
{
    public static ApiValidationError[] Instantiate(ValidationResult validationResult) =>
        validationResult.ToDictionary().Select(v => new ApiValidationError(propertyName: v.Key, messages: v.Value)).ToArray();

    public ApiValidationError(string propertyName, string[] messages)
    {
        PropertyName = propertyName;

        Messages = messages;

    }

    public string PropertyName { get; private set; }

    public string[] Messages { get; private set; }
}
