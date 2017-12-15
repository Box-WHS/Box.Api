using Box.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Api.Data.DataContexts.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Core.Data.User>
    {
        /// <inheritdoc />
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( "User" );

            builder.HasKey(u => u.Id);

            builder.Property( u => u.Guid )
                .IsRequired();
            
            builder.HasMany(u => u.Boxes)
                .WithOne(b => b.User);

            builder.HasMany(u => u.Cards)
                .WithOne(c => c.User);

            builder.HasMany(t => t.Trays)
                .WithOne(t => t.User);

            builder.HasIndex(u => u.Guid)
                .IsUnique();
            builder.HasIndex(u => u.Id);
        }
    }
}