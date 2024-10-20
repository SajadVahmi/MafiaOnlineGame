using IdentityProvider.Persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider.Persistence.DbContexts;

public class IdpDbContext(DbContextOptions<IdpDbContext> options) : IdentityDbContext<IdpUser>(options);