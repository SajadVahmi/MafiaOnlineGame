using Framework.Core.Contracts;

namespace Framework.Test.Stubs;

public class IdGeneratorStub(string id) : IIdGenerator
{
    public static IdGeneratorStub Instantiate() =>
        new IdGeneratorStub();

    public IdGeneratorStub() : this(Guid.NewGuid().ToString())
    {
    }


    public string GetNew()
    {
        return id;
    }
}
