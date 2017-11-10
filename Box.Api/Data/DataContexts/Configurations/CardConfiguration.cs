﻿using Box.Core.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Box.Api.Data.DataContexts.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        /// <inheritdoc />
        public void Configure( EntityTypeBuilder<Card> builder )
        {
            builder.ToTable( "Card" );

            builder.HasKey( c => c.Id );

            builder.Property( c => c.ConcurrencyToken )
                //.ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            builder.Property( c => c.Answer )
                .HasMaxLength( 4000 )
                .IsRequired();

            builder.Property( c => c.Question )
                .HasMaxLength( 4000 )
                .IsRequired();

            builder.Property( c => c.LastProcessed )
                .IsRequired();

            builder.HasOne( c => c.Tray )
                .WithMany( t => t.Cards )
                .HasForeignKey( c => c.Id )
                .OnDelete( DeleteBehavior.Cascade );
        }
    }
}