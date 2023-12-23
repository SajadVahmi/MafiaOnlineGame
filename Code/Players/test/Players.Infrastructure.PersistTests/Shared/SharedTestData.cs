namespace Players.Infrastructure.PersistTests.Shared;

public static class SharedTestData
{
    public static class AuthenticatedUser
    {
        public static string Sub = "45658525";

        public static string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) ";

        public static string UserIP = "127.0.0.1";

        public static string UserName = "TestUserName";

        public static bool IsCurrentUser = true;
    }

    public static class DateTime
    {
        public static DateTimeOffset CurrentDateTime = new DateTimeOffset(
            year: 2023,
            month: 1,
            day: 1,
            hour: 0,
            minute: 0,
            second: 0,
            TimeSpan.Zero);
    }
}
