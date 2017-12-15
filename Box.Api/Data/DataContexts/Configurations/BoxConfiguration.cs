using Box.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Api.Data.DataContexts.Configurations
{
    public class BoxConfiguration : IEntityTypeConfiguration<Core.Data.Box>
    {
        /// <inheritdoc />
        public void Configure( EntityTypeBuilder<Core.Data.Box> builder )
        {
            builder.ToTable( "Box" );

            builder.HasKey( b => b.Id );

            builder.Property( b => b.Name )
                .HasMaxLength( 255 )
                .IsRequired();

            builder.HasMany( b => b.Trays )
                .WithOne( t => t.Box )
                .HasForeignKey( b => b.Id )
                .OnDelete( DeleteBehavior.Restrict );
        }
    }
}