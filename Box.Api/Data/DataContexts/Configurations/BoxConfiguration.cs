using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Api.Data.DataContexts.Configurations
{
    public class BoxConfiguration : IEntityTypeConfiguration<Core.DataTransferObjects.Box>
    {
        /// <inheritdoc />
        public void Configure( EntityTypeBuilder<Core.DataTransferObjects.Box> builder )
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

            builder.Property( b => b.ConcurrencyToken )
                //.ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            builder.HasOne( b => b.User )
                .WithMany( u => u.Boxes )
                .HasForeignKey( b => b.UserId )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}