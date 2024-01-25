namespace Players.SharedTestClasess.PlayerAggregate.Data;

public static class PlayerApiTestData
{
    public static string PlayerBcScopeName = "players";

    public static class Endpoints
    {
        public static string Registration = "/players";
        public static string ChangeProfile = "/players/{playerId}";
        public static string View = "/players/{playerId}";
    }



}
