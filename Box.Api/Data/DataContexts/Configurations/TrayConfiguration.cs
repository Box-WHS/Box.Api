using Box.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Api.Data.DataContexts.Configurations
{
    public class TrayConfiguration : IEntityTypeConfiguration<Tray>
    {
        /// <inheritdoc />
        public void Configure( EntityTypeBuilder<Tray> builder )
        {
            builder.ToTable( "Tray" );

            builder.HasKey(t => t.Id);

            builder.Property( t => t.Interval )
                .IsRequired();

            builder.Property(t => t.Name)
                .HasMaxLength(255)
                .IsRequired();
            
            builder.HasMany( t => t.Cards )
                .WithOne( c => c.Tray )
                .HasForeignKey( c => c.Id );

            builder.HasOne( t => t.Box )
                .WithMany( b => b.Trays )
                .HasForeignKey( t => t.Id );
        }
    }
}