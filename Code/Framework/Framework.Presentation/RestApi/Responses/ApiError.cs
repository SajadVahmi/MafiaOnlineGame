using FluentValidation.Results;
using Framework.Core.Domain.Exceptions;

namespace Framework.Presentation.RestApi.Responses;

public class ApiError(string message, IDictionary<string, string[]>? metaData = null)
{

    public static ApiError Instantiate(BusinessException exception) => new(message: exception.Message);

    public static ApiError Instantiate(string code, string message, ValidationResult validationResult) => new(message, validationResult.ToDictionary());

    public static ApiError Instantiate(string code, string message) => new(message);



    public string Message { get; private set; } = message;

    public IDictionary<string, string[]>? MetaData { get; set; } = metaData;
}