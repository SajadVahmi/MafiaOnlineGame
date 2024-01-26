using FluentValidation.Results;
using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Domian.Exceptions;

namespace Framework.Presentation.RestApi;

public class ApiError(string code, string message, IDictionary<string, string[]>? metaData = null)
{
   
    public static ApiError Instantiate(BusinessException exception) =>new(code:exception.Code,message:exception.Message);

    public static ApiError Instantiate(ApplicationServicesException exception) => new(code: exception.Code, message: exception.Message);

    public static ApiError Instantiate(string code,string message,ValidationResult validationResult) => new(code,message, validationResult.ToDictionary());
    
    public static ApiError Instantiate(string code,string message) => new(code,message);

  
    public  string Code { get; private set; } = code;

    public  string Message { get; private set; } = message;

    public IDictionary<string, string[]>? MetaData { get; set; } = metaData;
}
