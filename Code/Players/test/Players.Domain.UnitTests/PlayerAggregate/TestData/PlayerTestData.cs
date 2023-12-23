using Players.Contracts.Enums;
using Players.Domain.PlayerAggregate.Models;

namespace Players.Domain.UnitTests.PlayerAggregate.TestData;

public static class PlayerTestData
{
    public static class RandomPlayer
    {
        public static PlayerId Id = PlayerId.Instantiate(Faker.RandomNumber.Next(1, 100));

        public static string FirstName = Faker.Name.First();

        public static string LastName = Faker.Name.Last();

        public static DateOnly BirthDate = new DateOnly(
            year: Faker.Identification.DateOfBirth().Year,
            month: Faker.Identification.DateOfBirth().Month,
            day: Faker.Identification.DateOfBirth().Day);

        public static Gender Gender = Faker.Enum.Random<Gender>();

        public static string UserId = Faker.RandomNumber.Next(1000, 2000).ToString();
    }

    public static class Sajad
    {
        public static PlayerId Id = PlayerId.Instantiate(1);

        public static string FirstName = "Sajad";

        public static string LastName = "Vahmi";

        public static DateOnly BirthDate = new DateOnly(1991, 03, 01);

        public static Gender Gender = Gender.Male;

        public static string UserId = "1234";
    }
}
