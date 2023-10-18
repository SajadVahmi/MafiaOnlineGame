namespace Framework.Core.Domian.Exceptions;

public class BusinessException:Exception
{
    public string Code { get;private set; }

    public BusinessException(string message,string code):base(message)
    {
        Code = code;
    }
}
