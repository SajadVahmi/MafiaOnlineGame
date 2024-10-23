using EventStore.Client;

namespace Framework.Projection;

public interface ICursor
{
    Position CurrentPosition();
    void MoveTo(Position position);
}