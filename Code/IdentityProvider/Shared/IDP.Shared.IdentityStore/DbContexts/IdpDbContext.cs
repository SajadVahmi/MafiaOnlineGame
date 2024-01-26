using IDP.Shared.IdentityStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IDP.Shared.IdentityStore.DbContexts;

public class IdpDbContext(DbContextOptions<IdpDbContext> options) : IdentityDbContext<IdpUser>(options);