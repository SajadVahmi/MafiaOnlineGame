using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;
using Players.Infrastructure.PersistTests.Shared;

namespace Players.Infrastructure.PersistTests.PlayerAggregate.TestData;

public static class PlayerAggregateTestData
{
    public static class Sajad
    {
        public static PlayerId Id = PlayerId.Instantiate(110);

        public static string FirstName = "Sajad";

        public static string LastName = "Vahmi";

        public static DateOnly BirthDate = new DateOnly(1991, 3, 1);

        public static Gender Gender = Gender.Male;

        public static string UserId = SharedTestData.AuthenticatedUser.Sub;
    }
}
