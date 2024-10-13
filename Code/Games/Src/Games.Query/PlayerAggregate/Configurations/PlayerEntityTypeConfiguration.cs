using Games.Query._Shared.Constants;
using Games.Query.PlayerAggregate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Games.Query.PlayerAggregate.Configurations;

public class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<PlayerQueryModel>
{
    public void Configure(EntityTypeBuilder<PlayerQueryModel> builder)
    {
        builder.ToTable(TableNames.Player);

    }
}