using Players.Domain.PlayerAggregate.Events;

namespace Players.Domain.PlayerAggregate.Models;

public partial class Player
{   
    private void When(PlayerIsRegistred domainEvent)
    {
        Id = PlayerId.Instantiate(domainEvent.PlayerId);

        FirstName = domainEvent.FirstName;

        LastName = domainEvent.LastName;

        BirthDate = domainEvent.BirthDate;

        Gender = domainEvent.Gender;

        RegisterDateTime = domainEvent.RegisterDateTime;

        UserId = domainEvent.UserId;
    }

    private void When(PlayerProfileChanged domainEvent)
    {
        FirstName = domainEvent.FirstName;

        LastName = domainEvent.LastName;

        BirthDate = domainEvent.BirthDate;

        Gender = domainEvent.Gender;
    }
}
