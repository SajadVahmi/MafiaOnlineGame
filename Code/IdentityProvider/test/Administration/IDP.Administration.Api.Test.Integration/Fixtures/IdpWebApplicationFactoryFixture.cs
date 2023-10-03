using IDP.Shared.IdentityStore.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration.Fixtures
{
    [CollectionDefinition("IdpWebApp Collection")]
    public class IdpWebApplicationFactoryFixture :ICollectionFixture<CustomWebApplicationFactory<Program>>
    {

    }
}
