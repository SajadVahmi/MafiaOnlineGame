using Framework.Core.ApplicationServices.Exceptions;
using Framework.Core.Domian.Exceptions;
using System.Globalization;
using System.Resources;

namespace Framework.Presentation.RestApi;

public class ApiError
{
    public static ApiError Instantiate(BusinessException exception) =>new(code:exception.Code,message:exception.Message);

    public static ApiError Instantiate(ApplicationServicesException exception) => new(code: exception.Code, message: exception.Message);


    public static ApiError Instantiate(BusinessException exception, ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        string message= resourceManager.GetString(exception.Message, cultureInfo)?? "Message not specified";

        return new(code:exception.Code,message:message);
    }

    public ApiError(string code, string message)
    {
        Code = code;

        Message = message;
    }

    public  string Code { get; private set; }

    public  string Message { get; private set; }
    
}
