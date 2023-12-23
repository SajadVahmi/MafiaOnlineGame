using Players.Contracts.Enums;

namespace Players.RestApi.IntegrationTests.V1.PlayerAggregates.TestData;

public class PlayerTestData
{
    public static class Endpoints
    {
        public static string Registration = "/players";
    }
    public static class Sajad
    {
        public static string Id = "223565896";

        public static string FirstName = "Sajad";

        public static string LastName = "Vahmi";

        public static DateOnly BirthDate = new DateOnly(1991, 3, 1);

        public static Gender Gender = Gender.Male;
    }
    public static class Somebody
    {

        public static string FirstName = Faker.Name.First();

        public static string LastName = Faker.Name.Last();

        public static DateOnly BirthDate = DateOnly.FromDateTime(Faker.Identification.DateOfBirth());

        public static Gender Gender = Faker.Enum.Random<Gender>();

    }
}
