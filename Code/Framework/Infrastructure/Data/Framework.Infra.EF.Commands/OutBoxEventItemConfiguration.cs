using Framework.Infra.OutBox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Infra.EF.Commands
{
    public class OutBoxEventItemConfiguration:IEntityTypeConfiguration<OutBoxEventItem>
    {
        public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
        {

            builder.ToTable($"{nameof(OutBoxEventItem)}s");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.AccuredByUserId).HasMaxLength(255);
            builder.Property(c => c.EventName).HasMaxLength(255);
            builder.Property(c => c.AggregateName).HasMaxLength(255);
            builder.Property(c => c.EventTypeName).HasMaxLength(500);
            builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
        }
    }

   
}
