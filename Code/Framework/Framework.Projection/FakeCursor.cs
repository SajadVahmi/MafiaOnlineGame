using EventStore.Client;

namespace Framework.Projection;

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