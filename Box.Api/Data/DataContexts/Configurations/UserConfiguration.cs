using System;
using Box.Core.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Api.Data.DataContexts.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( "User" );

            builder.HasKey( u => u.Id );

            builder.Property( u => u.Username )
                .IsRequired()
                .HasMaxLength( 255 );

            builder.Property( u => u.Email )
                .HasMaxLength( 255 )
                .IsRequired();

            builder.HasMany( u => u.Boxes )
                .WithOne( b => b.User )
                .HasForeignKey( b => b.Id )
                .OnDelete( DeleteBehavior.Restrict );

            builder.Property( u => u.ConcurrencyToken )
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValue( Guid.NewGuid() )
                .IsConcurrencyToken();
        }
    }
}