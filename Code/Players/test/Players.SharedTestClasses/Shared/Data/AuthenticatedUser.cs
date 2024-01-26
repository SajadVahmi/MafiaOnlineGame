namespace Players.SharedTestClasses.Shared.Data;

public static class UserTestData
{
    public static class AuthenticatedUser
    {
        public static string Sub = "45658525";

        public static string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) ";

        public static string UserIp = "127.0.0.1";

        public static string UserName = "TestUserName";

        public static bool IsCurrentUser = true;
    }

    public static class AuthenticatedSajad
    {
        public static string Sub = "999999999";

        public static string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) ";

        public static string UserIP = "127.0.0.1";

        public static string UserName = "SajadVahmi";

        public static bool IsCurrentUser = true;
    }
}
