using EventStore.Client;

namespace Backgrounds.Projection.Sql._Shared;

public class FakeCursor : ICursor
{
    public Position CurrentPosition()
    {
        return Position.Start;
    }

    public void MoveTo(Position position)
    {
        throw new System.NotImplementedException();
    }
}