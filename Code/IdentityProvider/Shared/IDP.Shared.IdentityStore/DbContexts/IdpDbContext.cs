using IDP.Shared.IdentityStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IDP.Shared.IdentityStore.DbContexts
{
    public class IdpDbContext: IdentityDbContext<IdpUser>
    {
        public IdpDbContext(DbContextOptions<IdpDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
