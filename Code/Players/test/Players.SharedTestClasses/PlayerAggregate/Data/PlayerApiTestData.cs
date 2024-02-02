namespace Players.SharedTestClasses.PlayerAggregate.Data;

public static class PlayerApiTestData
{
    public static string PlayerBcScopeName = "players";

    public static class Endpoints
    {
        public static class Version1
        {
            public static string Registration = "/v1/players";
            public static string ChangeProfile = "/v1/players/{playerId}";
            public static string View = "/v1/players/{playerId}";
        }
    }



}
