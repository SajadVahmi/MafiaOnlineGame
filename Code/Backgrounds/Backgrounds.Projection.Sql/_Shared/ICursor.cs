using EventStore.Client;

namespace Backgrounds.Projection.Sql._Shared;

public interface ICursor
{
    Position CurrentPosition();
    void MoveTo(Position position);
}