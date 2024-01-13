using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;
using Players.SharedTestClasess.Shared.Data;

namespace Players.SharedTestClasess.PlayerAggregate.Data;

public static class PlayerTestData
{
   
    public static class SomeBody
    {
        public static PlayerId Id => PlayerId.Instantiate(Faker.RandomNumber.Next(1, 100000));

        public static string FirstName => Faker.Name.First();

        public static string LastName => Faker.Name.Last();

        public static DateOnly BirthDate => DateOnly.FromDateTime(Faker.Identification.DateOfBirth());

        public static Gender Gender => Faker.Enum.Random<Gender>();

        public static string UserId => Faker.RandomNumber.Next(1000, 200000).ToString();
    }

    public static class Sajad
    {
        public static PlayerId Id = PlayerId.Instantiate(1);

        public static string FirstName = "Sajad";

        public static string LastName = "Vahmi";

        public static DateOnly BirthDate = DateOnly.FromDateTime(DateTimeTestData.Friday1March1991.Date);

        public static Gender Gender = Gender.Male;

        public static string UserId = AuthenticatedSajad.Sub;
    }
}
