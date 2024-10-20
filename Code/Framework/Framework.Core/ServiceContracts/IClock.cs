namespace Framework.Core.ServiceContracts;

public interface IClock
{
    public DateTimeOffset Now();

}