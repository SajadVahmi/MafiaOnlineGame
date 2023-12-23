using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Players.Domain.PlayerAggregate.Models;
using Players.Persistence.SQL.Constants;
using System.Reflection.Emit;

namespace Players.Persistence.SQL.Configurations;

public class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(Names.PlayersTable);

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, id => PlayerId.Instantiate(id));

    }

   
}
