using EventStore.Client;

namespace Framework.Projection;

public class Cursor : ICursor
{
    private Position _position;
    public Cursor() => _position = Position.Start;
    public Cursor(ulong position) => _position =new Position(position,position);

    public Position CurrentPosition()
    {
        return _position;
    }

    public void MoveTo(Position position)
    {
        _position=position;
    }
}