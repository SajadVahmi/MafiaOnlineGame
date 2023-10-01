using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Administration.Api.Test.Integration.Fixtures
{
    [CollectionDefinition("IdpWebApp collection")]
    public class IdpWebApplicationFactoryFixture : ICollectionFixture<IdpWebApplicationFactory<Program>>
    {
    
    }
}
