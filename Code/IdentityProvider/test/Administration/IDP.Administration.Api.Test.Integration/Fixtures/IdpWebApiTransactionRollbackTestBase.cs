using IDP.Shared.IdentityStore.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration.Fixtures
{
    [Collection("IdpWebApp Collection")]
    public class IdpWebApiTransactionRollbackTestBase
    {
        protected HttpClient Client;
        protected CustomWebApplicationFactory<Program> Factory;


        public IdpWebApiTransactionRollbackTestBase(CustomWebApplicationFactory<Program> factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
        }

       
    }
}
