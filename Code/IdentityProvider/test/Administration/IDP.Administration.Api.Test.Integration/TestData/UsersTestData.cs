using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration.TestData
{
    public static class UsersTestData
    {
       private static Random Random = new Random();

        public static class Endpoints
        {
            public static string Create = "/users";
        }
       public static class Sajad
       {
            public static string Email = "sajad.vahmi@gmail.com";
            public static string PhoneNumber = "09387607524";
        }
        public static class Somebody
        {
            public static string Email =Faker.Internet.Email();
            public static string PhoneNumber = $"0918{Random.Next(0,9)}{Random.Next(0, 9)}{Random.Next(0, 9)}{Random.Next(0, 9)}{Random.Next(0, 9)}{Random.Next(0, 9)}{Random.Next(0, 9)}";

        }
    }
}
