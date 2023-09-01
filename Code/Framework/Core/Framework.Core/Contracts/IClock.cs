namespace Framework.Core.Contracts;

public interface IClock
{
    public DateTimeOffset Now();
}